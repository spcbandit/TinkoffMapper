using Grpc.Core;
using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using Grpc.Net.Client.Web;
using StreamManager.Data;
using Tinkoff.Proto.InvestApi.V1;
using TinkoffMapper.Invest.Stream.Data;
using System.Threading.Tasks;
using System.Linq;

namespace TinkoffMapper.Invest.UserStreams
{
    public delegate void PortfolioSubscriptionDelegate(PortfolioSubscriptionResult portfolioSubscription);
    public delegate void PositionsSubscriptionDelegate(PositionsSubscriptionResult positionsSubscription);
    public delegate void PortfolioResponseDelegate(PortfolioResponse portfolio);
    public delegate void PositionDataDelegate(PositionData position);
    public delegate void OrderTradesDelegate(OrderTrades trades);

    public delegate void PingDelegate(Ping response);

    public delegate void OnCloseDelegate(object sender, StreamCloseMessage message);
    public delegate void OnErrorDelegate(object sender, StreamErrorMessage message);
    public delegate void OnOpenDelegate(object sender);

    public class CombineUserSubscriber
    {
        private GrpcChannel operationsChannel;
        private GrpcChannel tradesChannel;

        private OrdersStreamService.OrdersStreamServiceClient ordersClient;
        private OperationsStreamService.OperationsStreamServiceClient portfolioClient;
        private OperationsStreamService.OperationsStreamServiceClient positionClient;

        private StreamingService<TradesStreamResponse> tradesStreamingService { get; set; }
        private StreamingService<PortfolioStreamResponse> portfolioStreamingService { get; set; }
        private StreamingService<PositionsStreamResponse> positionStreamingService { get; set; }

        private Metadata headers;
        private string accountId;
        private string apiKey;
        private string apiUrl;

        protected CancellationTokenSource cancelTokenSource;
        protected CancellationToken token;

        public event PortfolioSubscriptionDelegate PortfolioSubscriptionEvent;
        public event PositionsSubscriptionDelegate PositionsSubscriptionEvent;
        public event PortfolioResponseDelegate PortfolioResponseEvent;
        public event PositionDataDelegate PositionDataEvent;
        public event OrderTradesDelegate OrderTradesEvent;

        public event PingDelegate PingEvent;

        public event OnCloseDelegate OnCloseEvent;
        public event OnErrorDelegate OnErrorEvent;
        public event OnOpenDelegate OnOpenEvent;

        public bool IsAlive => streamStatus == StreamStatusEnum.OPEN;

        private StreamStatusEnum streamStatus;

        public CombineUserSubscriber(string apiKey, string apiUrl, string accountId)
        {
            if (string.IsNullOrEmpty(apiKey))
                throw new ArgumentException(nameof(apiKey));

            if (string.IsNullOrEmpty(apiUrl))
                throw new ArgumentException(nameof(apiUrl));

            if (string.IsNullOrEmpty(accountId))
                throw new ArgumentException(nameof(accountId));

            this.apiKey = apiKey;
            this.apiUrl = apiUrl;
            this.accountId = accountId;
        }

        /// <summary>
        /// Запуск стрима
        /// </summary>
        public void RunStream()
        {
            try
            {
                InitStream();
                streamStatus = StreamStatusEnum.OPEN;
                OnOpenEvent?.Invoke(this);

                tradesStreamingService.Run(NotifyOrderTrades);
                positionStreamingService.Run(NotifyPositions);
                portfolioStreamingService.Run(NotifyPortfolio);
            }
            catch (Exception e)
            {
                OnErrorEvent?.Invoke(this, new StreamErrorMessage(e.Message, e is RpcException));
            }
        }

        /// <summary>
        /// Иницализация каналов
        /// </summary>
        void InitChannels()
        {
            operationsChannel = GrpcChannel.ForAddress(apiUrl, new GrpcChannelOptions
            {
                HttpHandler = new GrpcWebHandler(new HttpClientHandler()),
            });

            tradesChannel = GrpcChannel.ForAddress(apiUrl, new GrpcChannelOptions
            {
                HttpHandler = new GrpcWebHandler(new HttpClientHandler()),
            });

            ordersClient = new OrdersStreamService.OrdersStreamServiceClient(tradesChannel);
            portfolioClient = new OperationsStreamService.OperationsStreamServiceClient(operationsChannel);
            positionClient = new OperationsStreamService.OperationsStreamServiceClient(operationsChannel);
        }

        /// <summary>
        /// Инициализация токена отмены
        /// </summary>
        void InitCancelToken()
        {
            cancelTokenSource = new CancellationTokenSource();
            token = cancelTokenSource.Token;
        }

        /// <summary>
        /// Инициализация стрима
        /// </summary>
        void InitStream()
        {
            try
            {
                InitChannels();
                InitCancelToken();

                headers = GetLoginData(apiKey);

                var tradesStreamRequest = TradesStreamSub(accountId);
                tradesStreamingService = new StreamingService<TradesStreamResponse>(ordersClient.TradesStream(tradesStreamRequest, headers, null, token), token);

                var portfolioStreamRequest = PortfolioStreamSub(accountId);
                portfolioStreamingService = new StreamingService<PortfolioStreamResponse>(portfolioClient.PortfolioStream(portfolioStreamRequest, headers, null, token), token);

                var positionsStreamRequest = PositionsStreamSub(accountId);
                positionStreamingService = new StreamingService<PositionsStreamResponse>(positionClient.PositionsStream(positionsStreamRequest, headers, null, token), token);
            }
            catch (Exception e)
            {
                streamStatus = StreamStatusEnum.CLOSE;
                OnErrorEvent?.Invoke(this, new StreamErrorMessage(e.Message, e is RpcException));
            }
        }

        // TODO
        // Пока под вопросом
        /// <summary>
        /// Установка прокси
        /// </summary>
        /*public void SetProxy(string url, string username, string password)
        {
            try
            {
                var _proxy = new WebProxy(url);
                _proxy.BypassProxyOnLocal = true;

                Uri _uri;
                string message;
                url.TryCreateUri(out _uri, out message);

                _proxy.Credentials = new NetworkCredential(username, password, string.Format("{0}:{1}", _uri.DnsSafeHost, _uri.Port));

                channel = GrpcChannel.ForAddress(apiUrl, new GrpcChannelOptions
                {
                    HttpHandler = new GrpcWebHandler(new HttpClientHandler())

                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }*/

        /// <summary>
        /// Остановка стрима
        /// </summary>
        public void StopStream()
        {
            tradesChannel?.Dispose();
            operationsChannel?.Dispose();

            if (token.CanBeCanceled)
            {
                cancelTokenSource?.Cancel();
                tradesStreamingService?.Stop();
                portfolioStreamingService?.Stop();
                positionStreamingService?.Stop();
            }
            streamStatus = StreamStatusEnum.CLOSE;
            OnCloseEvent?.Invoke(this, new StreamCloseMessage());
        }

        /// <summary>
        /// Авторизация
        /// </summary>
        Metadata GetLoginData(string apiToken)
        {
            return new Metadata
            {
                { "Authorization", $"Bearer {apiToken}" }
            };
        }

        /// <summary>
        /// Stream сделок пользователя
        /// </summary>
        /// <param name="account">Аккаунт</param>
        /// <returns></returns>
        TradesStreamRequest TradesStreamSub(string account)
        {
            var tradesStreamRequest = new TradesStreamRequest
            {
                Accounts = { account }
            };
            return tradesStreamRequest;
        }

        /// <summary>
        /// Stream обновлений портфеля
        /// </summary>
        /// <param name="account">Аккаунт</param>
        /// <returns></returns>
        PortfolioStreamRequest PortfolioStreamSub(string account)
        {
            var portfolioStreamRequest = new PortfolioStreamRequest
            {
                Accounts = { account }
            };
            return portfolioStreamRequest;
        }

        /// <summary>
        /// Stream обновлений информации по изменению позиций портфеля
        /// </summary>
        /// <param name="account">Аккаунт</param>
        /// <returns></returns>
        PositionsStreamRequest PositionsStreamSub(string account)
        {
            var positionsStreamRequest = new PositionsStreamRequest
            {
                Accounts = { account }
            };
            return positionsStreamRequest;
        }

        /// <summary>
        /// Уведомления стрима
        /// </summary>
        void NotifyPositions(PositionsStreamResponse positions)
        {
            if (positions != null)
            {
                switch (positions.PayloadCase)
                {
                    case PositionsStreamResponse.PayloadOneofCase.Subscriptions:
                        PositionsSubscriptionEvent?.Invoke(positions.Subscriptions);
                        break;
                    case PositionsStreamResponse.PayloadOneofCase.Position:
                        PositionDataEvent?.Invoke(positions.Position);
                        break;
                    case PositionsStreamResponse.PayloadOneofCase.Ping:
                        InvokePing(positions.Ping);
                        break;
                }
            }
        }

        /// <summary>
        /// Уведомления стрима
        /// </summary>
        void NotifyOrderTrades(TradesStreamResponse trades)
        {
            if (trades != null)
            {
                switch (trades.PayloadCase)
                {
                    case TradesStreamResponse.PayloadOneofCase.OrderTrades:
                        OrderTradesEvent?.Invoke(trades.OrderTrades);
                        break;
                    case TradesStreamResponse.PayloadOneofCase.Ping:
                        InvokePing(trades.Ping);
                        break;
                }
            }
        }

        /// <summary>
        /// Уведомления стрима
        /// </summary>
        void NotifyPortfolio(PortfolioStreamResponse portfolio)
        {
            if (portfolio != null)
            {
                switch (portfolio.PayloadCase)
                {
                    case PortfolioStreamResponse.PayloadOneofCase.Subscriptions:
                        PortfolioSubscriptionEvent?.Invoke(portfolio.Subscriptions);
                        break;
                    case PortfolioStreamResponse.PayloadOneofCase.Portfolio:
                        PortfolioResponseEvent?.Invoke(portfolio.Portfolio);
                        break;
                    case PortfolioStreamResponse.PayloadOneofCase.Ping:
                        InvokePing(portfolio.Ping);
                        break;
                }
            }
        }

        void InvokePing(Ping ping)
        {
            PingEvent?.Invoke(ping);
        }
    }
}
