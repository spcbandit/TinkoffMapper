using System;
using Grpc.Core;
using Tinkoff.Proto.InvestApi.V1;
using Grpc.Net.Client;
using StreamManager.Services;

namespace TinkoffMapper.Invest.MarketStreams
{
    public class CombineMarketSubscriber
    {
        private GrpcChannel channel;
        private MarketDataStreamService.MarketDataStreamServiceClient client;
        private AsyncDuplexStreamingCall<MarketDataRequest, MarketDataResponse> stream;

        public MarketStreamingService<MarketDataRequest, MarketDataResponse> MarketStreamingService { get; private set; }

        public CombineMarketSubscriber(string apiKey, string apiUrl)
        {
            if (string.IsNullOrEmpty(apiKey))
                throw new ArgumentException(nameof(apiKey));

            if (string.IsNullOrEmpty(apiUrl))
                throw new ArgumentException(nameof(apiUrl));
            
            channel = GrpcChannel.ForAddress(apiUrl);
            client = new MarketDataStreamService.MarketDataStreamServiceClient(channel);
            var headers = GetLoginData(apiKey);
            stream = client.MarketDataStream(headers);
            MarketStreamingService = new MarketStreamingService<MarketDataRequest, MarketDataResponse>(stream);
        }

        /// <summary>
        /// Данные для авторизации
        /// </summary>
        public Metadata GetLoginData(string apiToken)
        {
            return new Metadata
            {
                { "Authorization", $"Bearer {apiToken}" }
            };
        }

        /// <summary>
        /// Реквест запроса исторических свечей по инструменту
        /// </summary>
        /// <param name="figi">Идентификатор валюты</param>
        /// <param name="interval">интервал</param>
        /// <returns></returns>
        public MarketDataRequest CandlesHistorySub(string figi, SubscriptionInterval interval, SubscriptionAction subscription)
        {
            var requestCandle = new MarketDataRequest
            {
                SubscribeCandlesRequest = new SubscribeCandlesRequest()
                {
                    SubscriptionAction = subscription,
                    Instruments =
                        {
                                new CandleInstrument()
                                {
                                    Figi = figi,
                                    Interval=interval
                                }
                        }
                }
            };
            return requestCandle;
        }

        /// <summary>
        /// Реквест на цены последних сделок.
        /// </summary>
        /// <param name="figi">Идентификатор валюты</param>
        /// <returns></returns>
        public MarketDataRequest LastPricesSub(string figi, SubscriptionAction subscription)
        {
            var requestInfo = new MarketDataRequest
            {
                SubscribeLastPriceRequest = new SubscribeLastPriceRequest()
                {
                    SubscriptionAction = subscription,
                    Instruments =
                    {
                        new LastPriceInstrument()
                        {
                            Figi = figi,
                        }
                    }
                }
            };
            return requestInfo;
        }

        /// <summary>
        /// Реквест на стакан
        /// </summary>
        /// <param name="figi">Идентификатор валюты</param>
        /// <param name="depth">Глубина</param>
        /// <returns></returns>
        public MarketDataRequest OrderBookSub(string figi, int depth, SubscriptionAction subscription)
        {
            var requestOrderBook = new MarketDataRequest
            {
                SubscribeOrderBookRequest = new SubscribeOrderBookRequest()
                {
                    SubscriptionAction = subscription,
                    Instruments =
                    {
                        new OrderBookInstrument()
                        {
                            Figi = figi,
                            Depth = depth
                        }
                    }
                }
            };
            return requestOrderBook;
        }

        /// <summary>
        /// Реквест обезличенных сделок
        /// </summary>
        /// <param name="figi">Идентификатор валюты</param>
        /// <returns></returns>
        public MarketDataRequest TradesSub(string figi, SubscriptionAction subscription)
        {
            var requestTrade = new MarketDataRequest
            {
                SubscribeTradesRequest = new SubscribeTradesRequest()
                {
                    SubscriptionAction = subscription,
                    Instruments =
                    {
                        new TradeInstrument()
                        {
                            Figi = figi,
                        }
                    }
                }
            };
            return requestTrade;
        }

        /// <summary>
        /// Реквест статуса торгов по инструментам
        /// </summary>
        /// <param name="figi">Идентификатор инструмента</param>
        /// <returns></returns>
        public MarketDataRequest TradingStatusSub(string figi, SubscriptionAction subscription)
        {
            var requestInfo = new MarketDataRequest
            {
                SubscribeInfoRequest = new SubscribeInfoRequest()
                {
                    SubscriptionAction = subscription,
                    Instruments =
                    {
                        new InfoInstrument()
                        {
                            Figi = figi,
                        }
                    }
                }
            };
            
            return requestInfo;
        }
    }
}
