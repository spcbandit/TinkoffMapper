using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using Grpc.Core;
using Tinkoff.Proto.InvestApi.V1;
using Grpc.Net.Client;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Net.Client.Web;
using StreamManager.Data;
using TinkoffMapper.Extensions;
using TinkoffMapper.Invest.Rest.Common.Response;
using TinkoffMapper.Invest.Stream.Data;
using TinkoffMapper.Invest.Rest.Common.Data.Enum;

namespace TinkoffMapper.Invest.MarketStreams
{
    public delegate void SubscribeCandlesResponseDelegate(SubscribeCandlesResponse candles);
    public delegate void SubscribeInfoResponseDelegate(SubscribeInfoResponse info);
    public delegate void SubscribeOrderBookResponseDelegate(SubscribeOrderBookResponse orderBook);
    public delegate void SubscribeTradesResponseDelegate(SubscribeTradesResponse trades);
    public delegate void SubscribeLastPriceResponseDelegate(SubscribeLastPriceResponse lastPrice);

    public delegate void CandleDelegate(Candle candle);
    public delegate void LastPriceDelegate(LastPrice lastPrice);
    public delegate void OrderBookDelegate(OrderBook orderBook);
    public delegate void TradeDelegate(Trade trade);
    public delegate void TradingStatusDelegate(TradingStatus status);

    public delegate void PingDelegate(Ping response);

    public delegate void OnCloseDelegate(object sender, StreamCloseMessage message);
    public delegate void OnErrorDelegate(object sender, StreamErrorMessage message);
    public delegate void OnOpenDelegate(object sender);


    public class CombineMarketSubscriber
    {
        private GrpcChannel channel;
        private MarketDataStreamService.MarketDataStreamServiceClient client;
        private Metadata headers;
        private string apiKey;
        private string apiUrl;
        private StreamingService<MarketDataResponse> streamService;

        protected CancellationTokenSource cancelTokenSource;
        protected CancellationToken token;

        public event SubscribeCandlesResponseDelegate SubscribeCandlesResponseEvent;
        public event SubscribeInfoResponseDelegate SubscribeInfoResponseEvent;
        public event SubscribeOrderBookResponseDelegate SubscribeOrderBookResponseEvent;
        public event SubscribeTradesResponseDelegate SubscribeTradesResponseEvent;
        public event SubscribeLastPriceResponseDelegate SubscribeLastPriceResponseEvent;

        public event CandleDelegate CandleEvent;
        public event LastPriceDelegate LastPriceEvent;
        public event OrderBookDelegate OrderBookEvent;
        public event TradeDelegate TradeEvent;
        public event TradingStatusDelegate TradingStatusEvent;

        public event PingDelegate PingEvent;

        public event OnCloseDelegate OnCloseEvent;
        public event OnErrorDelegate OnErrorEvent;
        public event OnOpenDelegate OnOpenEvent;

        public bool IsAlive => streamStatus == StreamStatusEnum.OPEN;

        private StreamStatusEnum streamStatus;

        public CombineMarketSubscriber(string apiKey, string apiUrl)
        {
            if (string.IsNullOrEmpty(apiKey))
                throw new ArgumentException(nameof(apiKey));

            if (string.IsNullOrEmpty(apiUrl))
                throw new ArgumentException(nameof(apiUrl));

            this.apiKey = apiKey;
            this.apiUrl = apiUrl;
        }

        /// <summary>
        /// Запуск стрима
        /// </summary>
        public async Task RunStream(List<string> figi, OrderBookDepthEnum depth = OrderBookDepthEnum.FIFTY)
        {
            try
            {
                InitStream(figi, depth);
                streamStatus = StreamStatusEnum.OPEN;
                OnOpenEvent?.Invoke(this);
                await streamService.Run(Notify);
            }
            catch (Exception e)
            {
                if (e is RpcException)
                {
                    if (e.Message.Contains("StatusCode=\"Unavailable\""))
                        OnErrorEvent?.Invoke(this, new StreamErrorMessage("Stream stopped", true));
                    else OnErrorEvent?.Invoke(this, new StreamErrorMessage(e.Message));
                }

                else OnErrorEvent?.Invoke(this, new StreamErrorMessage(e.Message));

            }
        }

        /// <summary>
        /// Иницализация канала
        /// </summary>
        void InitChannel()
        {
            channel = GrpcChannel.ForAddress(apiUrl, new GrpcChannelOptions
            {
                HttpHandler = new GrpcWebHandler(new HttpClientHandler()),
            });

            client = new MarketDataStreamService.MarketDataStreamServiceClient(channel);
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
        void InitStream(List<string> figi, OrderBookDepthEnum depth = OrderBookDepthEnum.FIFTY)
        {
            try
            {
                InitChannel();
                InitCancelToken();

                headers = GetLoginData(apiKey);

                int _depth = int.Parse(depth.GetEnumMemberAttributeValue() ?? "50");

                var request = new MarketDataServerSideStreamRequest()
                {
                    SubscribeOrderBookRequest = OrderBookSub(figi, _depth, SubscriptionAction.Subscribe),
                    SubscribeTradesRequest = TradesSub(figi, SubscriptionAction.Subscribe),
                };

                streamService = new StreamingService<MarketDataResponse>(client.MarketDataServerSideStream(request, headers, null, token), token);
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
            channel?.Dispose();

            if (token.CanBeCanceled)
            {
                cancelTokenSource?.Cancel();
                streamService?.Stop();
            }
            streamStatus = StreamStatusEnum.CLOSE;
            OnCloseEvent?.Invoke(this, new StreamCloseMessage());
        }
        
        /// <summary>
        /// Данные для авторизации
        /// </summary>
        Metadata GetLoginData(string apiToken)
        {
            return new Metadata
            {
                { "Authorization", $"Bearer {apiToken}" }
            };
        }

        /// <summary>
        /// Реквест запроса исторических свечей по инструменту
        /// </summary>
        SubscribeCandlesRequest CandlesHistorySub(List<string> figi, SubscriptionInterval interval, SubscriptionAction subscription)
        {
            var instruments = figi
                .Select(x => new CandleInstrument() { Figi = x })
                .ToList();

            return new SubscribeCandlesRequest()
            {
                SubscriptionAction = subscription,
                Instruments = { instruments }
            };
        }

        /// <summary>
        /// Реквест на цены последних сделок.
        /// </summary>
        SubscribeLastPriceRequest LastPricesSub(List<string> figi, SubscriptionAction subscription)
        {
            var instruments = figi
                .Select(x => new LastPriceInstrument() { Figi = x })
                .ToList();

            return new SubscribeLastPriceRequest()
            {
                SubscriptionAction = subscription,
                Instruments = { instruments }
            };
        }

     
        /// <summary>
        /// Реквест на стакан
        /// </summary>
        SubscribeOrderBookRequest OrderBookSub(List<string> figi, int depth, SubscriptionAction subscription)
        {
            var instruments = figi
                .Select(x => new OrderBookInstrument() { Figi = x, Depth = depth })
                .ToList();

            return new SubscribeOrderBookRequest()
            {
                SubscriptionAction = subscription,
                Instruments = { instruments }
            };
        }

        /// <summary>
        /// Реквест обезличенных сделок
        /// </summary>
        SubscribeTradesRequest TradesSub(List<string> figi, SubscriptionAction subscription)
        {
            var instruments = figi
                .Select(x => new TradeInstrument() { Figi = x })
                .ToList();

            return new SubscribeTradesRequest()
            {
                SubscriptionAction = subscription,
                Instruments = { instruments }
            };
        }

        /// <summary>
        /// Реквест статуса торгов по инструментам
        /// </summary>
        SubscribeInfoRequest TradingStatusSub(List<string> figi, SubscriptionAction subscription)
        {
            var instruments = figi
                .Select(x => new InfoInstrument() { Figi = x })
                .ToList();

            return new SubscribeInfoRequest()
            {
                SubscriptionAction = subscription,
                Instruments = { instruments }
            };
        }

        /// <summary>
        /// Уведомления стрима
        /// </summary>
        void Notify(MarketDataResponse response)
        {
            switch (response.PayloadCase)
            {
                case MarketDataResponse.PayloadOneofCase.SubscribeInfoResponse:
                    SubscribeInfoResponseEvent?.Invoke(response.SubscribeInfoResponse);
                    break;
                case MarketDataResponse.PayloadOneofCase.TradingStatus:
                    TradingStatusEvent?.Invoke(response.TradingStatus);
                    break;
                case MarketDataResponse.PayloadOneofCase.SubscribeTradesResponse:
                    SubscribeTradesResponseEvent?.Invoke(response.SubscribeTradesResponse);
                    break;
                case MarketDataResponse.PayloadOneofCase.Trade:
                    TradeEvent?.Invoke(response.Trade);
                    break;
                case MarketDataResponse.PayloadOneofCase.SubscribeOrderBookResponse:
                    SubscribeOrderBookResponseEvent?.Invoke(response.SubscribeOrderBookResponse);
                    break;
                case MarketDataResponse.PayloadOneofCase.Orderbook:
                    OrderBookEvent?.Invoke(response.Orderbook);
                    break;
                case MarketDataResponse.PayloadOneofCase.SubscribeLastPriceResponse:
                    SubscribeLastPriceResponseEvent?.Invoke(response.SubscribeLastPriceResponse);
                    break;
                case MarketDataResponse.PayloadOneofCase.LastPrice:
                    LastPriceEvent?.Invoke(response.LastPrice);
                    break;
                case MarketDataResponse.PayloadOneofCase.SubscribeCandlesResponse:
                    SubscribeCandlesResponseEvent?.Invoke(response.SubscribeCandlesResponse);
                    break;
                case MarketDataResponse.PayloadOneofCase.Ping:
                    PingEvent?.Invoke(response.Ping);
                    break;
                case MarketDataResponse.PayloadOneofCase.Candle:
                    CandleEvent?.Invoke(response.Candle);
                    break;
            }
        }
    }
}
