using Grpc.Core;
using Grpc.Net.Client;
using NUnit.Framework;
using RestSharp;
using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tinkoff.Proto.InvestApi.V1;
using TinkoffMapper.Requests;
using TinkoffMapper.Invest.Rest.Data.Enum;
using TinkoffMapper.Invest.Rest.Data.Market;
using TinkoffTester.Data;
using TinkoffTester.Request;
using static Tinkoff.Proto.InvestApi.V1.MarketDataResponse;
using GetAccountsRequest = TinkoffMapper.Invest.Rest.Request.Account.GetAccountsRequest;
using OpenSandboxAccountRequest = TinkoffTester.Sandbox.Request.OpenSandboxAccountRequest;
using CloseSandboxAccountRequest = TinkoffTester.Sandbox.Request.CloseSandboxAccountRequest;
using OperationsRequest = TinkoffMapper.Invest.Rest.Request.Account.OperationsRequest;
using GetLastPricesRequest = TinkoffMapper.Invest.Rest.Request.Market.GetLastPricesRequest;
using SandboxPayInRequest = TinkoffTester.Sandbox.Request.SandboxPayInRequest;
using TinkoffMapper.Handlers;
using Newtonsoft.Json.Converters;
using TinkoffMapper.Invest.MarketStreams;
using TinkoffMapper.Invest.Rest;
using TinkoffMapper.Invest.Rest.Request.Account;
using TinkoffMapper.Invest.UserStreams;
using GetAccountsResponse = TinkoffMapper.Invest.Rest.Response.Account.GetAccountsResponse;
using GetLastPricesResponse = TinkoffMapper.Invest.Rest.Response.Market.GetLastPricesResponse;
using InstrumentResponse = TinkoffMapper.Invest.Rest.Response.Market.InstrumentResponse;
using PostOrderResponse = TinkoffMapper.Invest.Rest.Response.Account.PostOrderResponse;

namespace TinkoffTester
{
    public class TesterTinkoff
    {
        private string ApiToken = "t.xRSkthzZgUJ7LTg1nvaRq6J-SiMCEsRXHIcOE_2qKyazrmwvYhFLnzJ9wXxXB2TtsNTFhzPQkLw3W1xEc6VEfQ";
        private bool SandboxMode = true;
        private string pathApiRest = "https://invest-public-api.tinkoff.ru/rest";
        private string pathApi = "wss://api-invest.tinkoff.ru/openapi/md/v1/md-openapi/ws";
        private string pathApiGrpc = "https://invest-public-api.tinkoff.ru:443";

        private string pathApiGrpcSandbox = "https://sandbox-invest-public-api.tinkoff.ru:443";

        private RestClient client = null;

        public DateTime Start = DateTime.Now.AddDays(-11);
        public DateTime End = DateTime.Now.AddDays(-10);
        public string Figi = "FUTYNDF06230"; //Яндекс
        public int Depth = 20;
        public RestApiComposition InvestHandler;
        private RequestArranger requestArranger = null;
        private SandboxRequestArranger sandboxRequestArranger = null;
        private CombineMarketSubscriber combineMarket;

        public TesterTinkoff()
        {
            client = new RestClient(pathApiRest);
            InvestHandler = new RestApiComposition();
            requestArranger = new RequestArranger(ApiToken, null, SandboxMode);
            sandboxRequestArranger = new SandboxRequestArranger(ApiToken, null);
            combineMarket = new CombineMarketSubscriber(ApiToken, pathApiGrpcSandbox);
        }

        #region [PublicSocket]
        /// <summary>
        /// Запрос подписки на стаканы
        /// </summary>
        [Test]
        public void PublicSocketOrderBookSubscribe_Success()
        {
            var requestOrderBook = combineMarket.OrderBookSub(Figi, Depth, SubscriptionAction.Subscribe);

            combineMarket.MarketStreamingService.Subscribe(response =>
            {
                if (response.PayloadCase == PayloadOneofCase.SubscribeOrderBookResponse)
                {
                    var orderBookResponse = response.SubscribeOrderBookResponse;

                    Assert.IsNotNull(orderBookResponse);
                    Assert.IsNotNull(orderBookResponse.TrackingId);
                    Assert.IsTrue(orderBookResponse.OrderBookSubscriptions.Count > 0);


                    var subscription = orderBookResponse.OrderBookSubscriptions.FirstOrDefault();

                    Assert.IsNotNull(subscription);
                    Assert.AreEqual(subscription.Depth, Depth);
                    Assert.AreEqual(subscription.Figi, Figi);
                    Assert.AreEqual(subscription.SubscriptionStatus, SubscriptionStatus.Success);
                }
                if (response.PayloadCase == PayloadOneofCase.Orderbook)
                {
                    var orderBook = response.Orderbook;
                    Assert.IsNotNull(orderBook);
                    Assert.IsNotNull(orderBook.Time);

                    Assert.AreEqual(orderBook.Figi, Figi);
                    Assert.AreEqual(orderBook.Depth, Depth);

                    Assert.IsTrue(orderBook.Asks.Count > 0);
                    Assert.IsTrue(orderBook.Asks.Count > 0);

                    Assert.Pass();

                }
            }, requestOrderBook);
        }

        /// <summary>
        /// Запрос подписки на свечи
        /// </summary>
        [Test]
        public void PublicSocketCandleSubscribe_Success()
        {
            try
            {
                var requestCandle = combineMarket.CandlesHistorySub(Figi, SubscriptionInterval.FiveMinutes, SubscriptionAction.Subscribe);
                combineMarket.MarketStreamingService.Subscribe(response =>
                {
                    if (response.PayloadCase == PayloadOneofCase.SubscribeCandlesResponse)
                    {
                        var subscribeCandlesResponse = response.SubscribeCandlesResponse;

                        Assert.IsNotNull(subscribeCandlesResponse);
                        Assert.IsNotNull(subscribeCandlesResponse.TrackingId);
                        Assert.IsTrue(subscribeCandlesResponse.CandlesSubscriptions.Count > 0);

                        var subscription = subscribeCandlesResponse.CandlesSubscriptions.FirstOrDefault();

                        Assert.IsNotNull(subscription);
                        Assert.IsNotNull(subscription.Interval);
                        Assert.AreEqual(subscription.Figi, Figi);
                        Assert.AreEqual(subscription.SubscriptionStatus, SubscriptionStatus.Success);
                    }

                    if (response.PayloadCase == PayloadOneofCase.Candle)
                    {
                        var candle = response.Candle;
                        Assert.IsNotNull(candle);
                        Assert.AreEqual(candle.Figi, Figi);
                        Assert.IsNotNull(candle.Volume);
                        Assert.IsNotNull(candle.Time);
                        Assert.IsNotNull(candle.Open.Nano);
                        Assert.IsNotNull(candle.Open.Units);
                        Assert.IsNotNull(candle.Close.Nano);
                        Assert.IsNotNull(candle.Close.Units);
                        Assert.IsNotNull(candle.Low.Nano);
                        Assert.IsNotNull(candle.Low.Units);
                        Assert.IsNotNull(candle.High.Nano);
                        Assert.IsNotNull(candle.High.Units);
                        Assert.Pass();
                    }
                }, requestCandle);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        /// <summary>
        /// Запрос подписки на ленту обезличенных сделок
        /// </summary>
        [Test]
        public void PublicSocketTradesSubscribe_Success()
        {
            var requestTrades = combineMarket.TradesSub(Figi, SubscriptionAction.Subscribe);

            combineMarket.MarketStreamingService.Subscribe(response =>
            {
                if (response.PayloadCase == PayloadOneofCase.SubscribeTradesResponse)
                {
                    var subscribeTradesResponse = response.SubscribeTradesResponse;

                    Assert.IsNotNull(subscribeTradesResponse);
                    Assert.IsNotNull(subscribeTradesResponse.TrackingId);
                    Assert.IsTrue(subscribeTradesResponse.TradeSubscriptions.Count > 0);

                    var subscription = subscribeTradesResponse.TradeSubscriptions.FirstOrDefault();

                    Assert.IsNotNull(subscription);
                    Assert.AreEqual(subscription.Figi, Figi);
                    Assert.AreEqual(subscription.SubscriptionStatus, SubscriptionStatus.Success);
                }
                if (response.PayloadCase == PayloadOneofCase.Trade)
                {
                    var trade = response.Trade;
                    Assert.IsNotNull(trade);
                    Assert.AreEqual(trade.Figi, Figi);
                    Assert.IsNotNull(trade.Direction);
                    Assert.IsNotNull(trade.Quantity);
                    Assert.IsNotNull(trade.Time);
                    Assert.IsNotNull(trade.Price);

                    Assert.Pass();
                }
            }, requestTrades);
        }

        /// <summary>
        /// Запрос подписки на торговые статусы инструментов
        /// </summary>
        [Test]
        public void PublicSocketInfoSubscribe_Success()
        {
            var requestInfo = combineMarket.TradingStatusSub(Figi, SubscriptionAction.Subscribe);
            combineMarket.MarketStreamingService.Subscribe(response =>
            {
                if (response.PayloadCase == PayloadOneofCase.SubscribeInfoResponse)
                {
                    var subscribeInfoResponse = response.SubscribeInfoResponse;

                    Assert.IsNotNull(subscribeInfoResponse);
                    Assert.IsNotNull(subscribeInfoResponse.TrackingId);
                    Assert.IsTrue(subscribeInfoResponse.InfoSubscriptions.Count > 0);

                    var subscription = subscribeInfoResponse.InfoSubscriptions.FirstOrDefault();

                    Assert.IsNotNull(subscription);
                    Assert.AreEqual(subscription.Figi, Figi);
                    Assert.AreEqual(subscription.SubscriptionStatus, SubscriptionStatus.Success);

                }
                if (response.PayloadCase == PayloadOneofCase.TradingStatus)
                {
                    var tradingStatus = response.TradingStatus;
                    Assert.IsNotNull(tradingStatus);
                    Assert.IsNotNull(tradingStatus.Time);
                    Assert.IsNotNull(tradingStatus.TradingStatus_);
                    Assert.AreEqual(tradingStatus.Figi, Figi);
                    Assert.Pass();

                }
            }, requestInfo);

        }
        #endregion

        #region [PrivateSocket]
        /// <summary>
        /// Запрос подписки на обновления портфеля
        /// </summary>
        [Test]
        public void PrivateSocketPortfolioStreamSubscribe_Success()
        {
            var openAccountsRequest = new OpenSandboxAccountRequest();
            var openAccountsResponse = SendTest(null, openAccountsRequest);

            Assert.IsNotNull(openAccountsResponse);

            var getAccountsRequest = new GetAccountsRequest();
            var getAccountsResponse = InvestHandler.HandleGetAccountsResponse(SendTest(getAccountsRequest));

            Assert.IsNotNull(getAccountsResponse);
            Assert.IsTrue(getAccountsResponse.Accounts.Count > 0);

            var account = getAccountsResponse.Accounts.First();

            var combineUser = new CombineUserSubscriber(ApiToken, pathApiGrpc, account.Id);

            var userStreamingService = new UserStreamingService<PortfolioStreamRequest, PortfolioStreamResponse>(combineUser.portfolioStream);

            userStreamingService.Subscribe(response =>
            {
                if (response.PayloadCase == PortfolioStreamResponse.PayloadOneofCase.Portfolio)
                {
                    var orderPositionResponse = response.Portfolio;

                    Assert.IsNotNull(orderPositionResponse);
                    Assert.AreEqual(orderPositionResponse.AccountId, account.Id);
                    Assert.IsNotNull(orderPositionResponse.ExpectedYield);
                    Assert.IsNotNull(orderPositionResponse.Positions);
                    Assert.IsNotNull(orderPositionResponse.TotalAmountFutures);

                    Assert.Pass();
                }
                if (response.PayloadCase == PortfolioStreamResponse.PayloadOneofCase.Ping)
                {
                    var tradingStatus = response.Ping;
                    Assert.IsNotNull(tradingStatus);
                    Assert.IsNotNull(tradingStatus.Time);
                }
            });
            try
            {
                var getInstrumentResponse = GetInstruments();

                Assert.IsNotNull(getInstrumentResponse);
                Assert.IsTrue(getInstrumentResponse.Instruments.Count > 0);

                GetOrderBookResponse currentOrderBook = null;
                InstrumentData currentInstrumentData = null;
                foreach (var instrument in getInstrumentResponse.Instruments)
                {
                    var getOrderBookResponse = GetInstrumentOrderBook(instrument.Uid);

                    if (getOrderBookResponse.Asks.Count > 0 && getOrderBookResponse.Bids.Count > 0)
                    {
                        currentOrderBook = getOrderBookResponse;
                        currentInstrumentData = instrument;
                        break;
                    }
                }

                Assert.IsNotNull(currentOrderBook);
                Assert.IsNotNull(currentInstrumentData);

                var firstAsk = currentOrderBook.Asks.FirstOrDefault();
                Assert.IsNotNull(firstAsk);

                var firstBid = currentOrderBook.Bids.FirstOrDefault();
                Assert.IsNotNull(firstBid);

                var openAccountResponse = OpenSandboxAccount();

                Assert.IsNotNull(openAccountResponse);
                Assert.IsNotNull(openAccountResponse.AccountId);

                var sandboxPayInResponse = SandboxPayIn(openAccountResponse.AccountId, "RUB", "1000");
                Assert.IsNotNull(sandboxPayInResponse);

                var postOrderResponse = PostOrder(openAccountResponse.AccountId, currentInstrumentData, (int)firstAsk.Quantity,
                    (int)firstAsk.Price.Units, firstAsk.Price.Nano, OrderDirectionEnum.ORDER_DIRECTION_BUY);

                Assert.IsNotNull(postOrderResponse);
                Assert.IsNotNull(postOrderResponse.OrderId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        

        /// <summary>
        /// Запрос подписки на обновления информации по изменению позиций портфеля
        /// </summary>
        [Test]
        public void PrivateSocketPositionsStreamSubscribe_Success()
        {
            var openAccountsRequest = new OpenSandboxAccountRequest();
            var openAccountsResponse = SendTest(null, openAccountsRequest);

            Assert.IsNotNull(openAccountsResponse);

            var getAccountsRequest = new GetAccountsRequest();
            var getAccountsResponse = InvestHandler.HandleGetAccountsResponse(SendTest(getAccountsRequest));

            Assert.IsNotNull(getAccountsResponse);
            Assert.IsTrue(getAccountsResponse.Accounts.Count > 0);

            var account = getAccountsResponse.Accounts.First();

            var combineUser = new CombineUserSubscriber(ApiToken, pathApiGrpc, account.Id);

            var userStreamingService = new UserStreamingService<PositionsStreamRequest, PositionsStreamResponse>(combineUser.positionsStream);

            userStreamingService.Subscribe(response =>
            {
                if (response.PayloadCase == PositionsStreamResponse.PayloadOneofCase.Position)
                {
                    var orderPositionResponse = response.Position;

                    Assert.IsNotNull(orderPositionResponse);
                    Assert.AreEqual(orderPositionResponse.AccountId, account.Id);
                    Assert.IsNotNull(orderPositionResponse.Date);
                    Assert.IsNotNull(orderPositionResponse.Futures);
                    Assert.IsNotNull(orderPositionResponse.Money);
                    Assert.IsNotNull(orderPositionResponse.Options);
                    Assert.IsNotNull(orderPositionResponse.Securities);
                    Assert.Pass();
                }
                if (response.PayloadCase == PositionsStreamResponse.PayloadOneofCase.Ping)
                {
                    var tradingStatus = response.Ping;
                    Assert.IsNotNull(tradingStatus);
                    Assert.IsNotNull(tradingStatus.Time);
                }
            });
            try
            {
                var getInstrumentResponse = GetInstruments();

                Assert.IsNotNull(getInstrumentResponse);
                Assert.IsTrue(getInstrumentResponse.Instruments.Count > 0);

                GetOrderBookResponse currentOrderBook = null;
                InstrumentData currentInstrumentData = null;
                foreach (var instrument in getInstrumentResponse.Instruments)
                {
                    var getOrderBookResponse = GetInstrumentOrderBook(instrument.Uid);

                    if (getOrderBookResponse.Asks.Count > 0 && getOrderBookResponse.Bids.Count > 0)
                    {
                        currentOrderBook = getOrderBookResponse;
                        currentInstrumentData = instrument;
                        break;
                    }
                }

                Assert.IsNotNull(currentOrderBook);
                Assert.IsNotNull(currentInstrumentData);

                var firstAsk = currentOrderBook.Asks.FirstOrDefault();
                Assert.IsNotNull(firstAsk);

                var firstBid = currentOrderBook.Bids.FirstOrDefault();
                Assert.IsNotNull(firstBid);

                var openAccountResponse = OpenSandboxAccount();

                Assert.IsNotNull(openAccountResponse);
                Assert.IsNotNull(openAccountResponse.AccountId);

                var sandboxPayInResponse = SandboxPayIn(openAccountResponse.AccountId, "RUB", "1000");
                Assert.IsNotNull(sandboxPayInResponse);

                var postOrderResponse = PostOrder(openAccountResponse.AccountId, currentInstrumentData, (int)firstAsk.Quantity,
                    (int)firstAsk.Price.Units, firstAsk.Price.Nano, OrderDirectionEnum.ORDER_DIRECTION_BUY);

                Assert.IsNotNull(postOrderResponse);
                Assert.IsNotNull(postOrderResponse.OrderId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Запрос подписки на сделки пользователя
        /// </summary>
        [Test]
        public void PrivateSocketTradesStreamSubscribe_Success()
        {
            var openAccountsRequest = new OpenSandboxAccountRequest();
            var openAccountsResponse = SendTest(null, openAccountsRequest);

            Assert.IsNotNull(openAccountsResponse);

            var getAccountsRequest = new GetAccountsRequest();
            var getAccountsResponse = InvestHandler.HandleGetAccountsResponse(SendTest(getAccountsRequest));

            Assert.IsNotNull(getAccountsResponse);
            Assert.IsTrue(getAccountsResponse.Accounts.Count > 0);

            var account = getAccountsResponse.Accounts.First();

            var combineUser = new CombineUserSubscriber(ApiToken, pathApiGrpc, account.Id);

            var userStreamingService = new UserStreamingService<TradesStreamRequest, TradesStreamResponse>(combineUser.ordersStream);

            userStreamingService.Subscribe(response =>
            {
                if (response.PayloadCase == TradesStreamResponse.PayloadOneofCase.OrderTrades)
                {
                    var orderTradesResponse = response.OrderTrades;
                    
                    Assert.IsNotNull(orderTradesResponse);
                    Assert.AreEqual(orderTradesResponse.Figi, Figi);
                    Assert.IsNotNull(orderTradesResponse.CreatedAt);
                    Assert.IsNotNull(orderTradesResponse.Direction);
                    Assert.IsNotNull(orderTradesResponse.OrderId);
                    Assert.IsNotNull(orderTradesResponse.Trades);
                    Assert.Pass();
                }
                if (response.PayloadCase == TradesStreamResponse.PayloadOneofCase.Ping)
                {
                    var tradingStatus = response.Ping;
                    Assert.IsNotNull(tradingStatus);
                    Assert.IsNotNull(tradingStatus.Time);
                }
            });


            var getInstrumentResponse = GetInstruments();

            Assert.IsNotNull(getInstrumentResponse);
            Assert.IsTrue(getInstrumentResponse.Instruments.Count > 0);

            GetOrderBookResponse currentOrderBook = null;
            InstrumentData currentInstrumentData = null;
            foreach (var instrument in getInstrumentResponse.Instruments)
            {
                var getOrderBookResponse = GetInstrumentOrderBook(instrument.Uid);

                if (getOrderBookResponse.Asks.Count > 0 && getOrderBookResponse.Bids.Count > 0)
                {
                    currentOrderBook = getOrderBookResponse;
                    currentInstrumentData = instrument;
                    break;
                }
            }

            Assert.IsNotNull(currentOrderBook);
            Assert.IsNotNull(currentInstrumentData);

            var firstAsk = currentOrderBook.Asks.FirstOrDefault();
            Assert.IsNotNull(firstAsk);

            var firstBid = currentOrderBook.Bids.FirstOrDefault();
            Assert.IsNotNull(firstBid);

            var openAccountResponse = OpenSandboxAccount();

            Assert.IsNotNull(openAccountResponse);
            Assert.IsNotNull(openAccountResponse.AccountId);

            var sandboxPayInResponse = SandboxPayIn(openAccountResponse.AccountId, "RUB", "1000");
            Assert.IsNotNull(sandboxPayInResponse);

            var postOrderResponse = PostOrder(openAccountResponse.AccountId, currentInstrumentData, (int)firstAsk.Quantity,
                (int)firstAsk.Price.Units, firstAsk.Price.Nano, OrderDirectionEnum.ORDER_DIRECTION_BUY);

            Assert.IsNotNull(postOrderResponse);
            Assert.IsNotNull(postOrderResponse.OrderId);
        }

        #endregion

        #region [PublicRest]
        /// <summary>
        /// Запрос исторических свечей по инструменту
        /// </summary>
        [Test]
        public void PublicRestGetCandles_Success()
        {
            var getInstrumentResponse = GetInstruments();

            Assert.IsNotNull(getInstrumentResponse);
            Assert.IsTrue(getInstrumentResponse.Instruments.Count > 0);

            var instrument = getInstrumentResponse.Instruments.FirstOrDefault(x=> x.Figi == Figi);
            Assert.IsNotNull(instrument);

            var candlesRequest = new TinkoffMapper.Invest.Rest.Request.Market.GetCandlesRequest(Start, End, CandleIntervalEnum.CANDLE_INTERVAL_5_MIN, instrument.Uid);
            var getCandlesResponse = InvestHandler.HandleGetCandlesResponse(SendTest(candlesRequest));

            Assert.IsNotNull(getCandlesResponse);
            Assert.IsTrue(getCandlesResponse.Candles.Count > 0);

            var candle = getCandlesResponse.Candles.FirstOrDefault();

            Assert.IsNotNull(candle);
            Assert.IsNotNull(candle.High.Nano);
            Assert.IsNotNull(candle.High.Units);
            Assert.IsNotNull(candle.Low.Nano);
            Assert.IsNotNull(candle.Low.Units);
            Assert.IsNotNull(candle.Open.Nano);
            Assert.IsNotNull(candle.Open.Units);
            Assert.IsNotNull(candle.Close.Nano);
            Assert.IsNotNull(candle.Close.Units);
            Assert.IsNotNull(candle.Time);
            Assert.IsNotNull(candle.Volume);
        }

        /// <summary>
        /// Запрос списка инструментов
        /// </summary>
        [Test]
        public void PublicRestInstrumentRequest_Success()
        {
            var getInstrumentResponse = GetInstruments();

            Assert.IsNotNull(getInstrumentResponse);
            Assert.IsTrue(getInstrumentResponse.Instruments.Count > 0);

            var instrument = getInstrumentResponse.Instruments.FirstOrDefault(x=>x.TradingStatusEnum == SecurityTradingStatusEnum.SECURITY_TRADING_STATUS_SESSION_OPEN);

            Assert.IsNotNull(instrument);
            Assert.IsNotNull(instrument.AssetType);
            Assert.IsNotNull(instrument.BasicAsset);
            Assert.IsNotNull(instrument.CountryOfRiskName);
            Assert.IsNotNull(instrument.Currency);
            Assert.IsNotNull(instrument.Exchange);
            Assert.IsNotNull(instrument.Name);
            Assert.IsNotNull(instrument.FuturesType);
            Assert.IsNotNull(instrument.Ticker);
            Assert.IsNotNull(instrument.ClassCode);
            Assert.IsNotNull(instrument.TradingStatus);
            Assert.IsNotNull(instrument.CountryOfRisk);

            Assert.IsNotNull(instrument.BasicAssetSize.Nano);
            Assert.IsNotNull(instrument.BasicAssetSize.Units);
            Assert.IsNotNull(instrument.Dlong.Nano);
            Assert.IsNotNull(instrument.Dlong.Units);
            Assert.IsNotNull(instrument.DlongMin.Nano);
            Assert.IsNotNull(instrument.DlongMin.Units);
            Assert.IsNotNull(instrument.DshortMin.Nano);
            Assert.IsNotNull(instrument.DshortMin.Units);
            Assert.IsNotNull(instrument.Dshort.Nano);
            Assert.IsNotNull(instrument.Dshort.Units);
            Assert.IsNotNull(instrument.Klong.Nano);
            Assert.IsNotNull(instrument.Klong.Units);
            Assert.IsNotNull(instrument.Kshort.Nano);
            Assert.IsNotNull(instrument.MinPriceIncrement.Units);
            Assert.IsNotNull(instrument.MinPriceIncrement.Nano);

            Assert.IsNotNull(instrument.ExpirationDate);
            Assert.IsNotNull(instrument.FirstTradeDate);
            Assert.IsNotNull(instrument.LastTradeDate);

            Assert.IsNotNull(instrument.TradingStatusEnum);
        }
        
        /// <summary>
        /// Запрос списка фьючерсов
        /// </summary>
        [Test]
        public void PublicRestLastPriceRequest_Success()
        {
            var getInstrumentResponse = GetInstruments();

            Assert.IsNotNull(getInstrumentResponse);
            Assert.IsTrue(getInstrumentResponse.Instruments.Count > 0);

            var instrument = getInstrumentResponse.Instruments.FirstOrDefault();
            Assert.IsNotNull(instrument);

            var getLastPricesResponse = GetInstrumentLastPrice(instrument.Uid);

            Assert.IsNotNull(getLastPricesResponse);
            Assert.IsNotNull(getLastPricesResponse.Prices);
        }

        /// <summary>
        /// Запрос 	стакана
        /// </summary>
        [Test]
        public void PublicRestGetOrderBookRequest_Success()
        {
            var getInstrumentResponse = GetInstruments();

            Assert.IsNotNull(getInstrumentResponse);
            Assert.IsTrue(getInstrumentResponse.Instruments.Count > 0);

            var instrument = getInstrumentResponse.Instruments.FirstOrDefault();
            Assert.IsNotNull(instrument);
            
            var getOrderBookResponse = GetInstrumentOrderBook(instrument.Uid);

            Assert.IsNotNull(getOrderBookResponse);
            Assert.IsNotNull(getOrderBookResponse.Figi);
            Assert.IsNotNull(getOrderBookResponse.ClosePriceTs);
            Assert.IsNotNull(getOrderBookResponse.Depth);
            Assert.IsNotNull(getOrderBookResponse.LastPrice);
            Assert.IsNotNull(getOrderBookResponse.LastPriceTs);
            Assert.IsNotNull(getOrderBookResponse.LimitDown);
            Assert.IsNotNull(getOrderBookResponse.LimitUp);
            Assert.IsNotNull(getOrderBookResponse.InstrumentUid);
            Assert.IsNotNull(getOrderBookResponse.OrderbookTs);

            Assert.IsTrue(getOrderBookResponse.Asks.Count > 0);
            Assert.IsTrue(getOrderBookResponse.Bids.Count > 0);
        }

        /// <summary>
        /// Получение списка инструментов(фьючерсов)
        /// </summary>
        /// <param name="status">Базовый список инструментов (по умолчанию)</param>
        /// <returns></returns>
        private InstrumentResponse GetInstruments(InstrumentStatusEnum status = InstrumentStatusEnum.INSTRUMENT_STATUS_BASE)
        {
            var instrumentRequest = new TinkoffMapper.Invest.Rest.Request.Market.InstrumentRequest(status, InstrumentTypeEnum.Currencies);
            return InvestHandler.HandleInstrumentResponse(SendTest(instrumentRequest));
        }

        /// <summary>
        /// Метод запроса цен последних сделок по инструментам.
        /// </summary>
        /// <param name="instrumentId">Внутренний идентификатор финансового инструмента</param>
        /// <returns></returns>
        private GetLastPricesResponse GetInstrumentLastPrice(string instrumentId)
        {
            var getLastPricesRequest = new GetLastPricesRequest(instrumentId);
            return InvestHandler.HandleGetLastPricesResponse(SendTest(getLastPricesRequest));
        }

        /// <summary>
        /// Метод получения стакана по инструменту
        /// </summary>
        /// <param name="instrumentId">Внутренний идентификатор финансового инструмента</param>
        /// <returns></returns>
        private GetOrderBookResponse GetInstrumentOrderBook(string instrumentId)
        {
            var getOrderBookRequest = new TinkoffMapper.Invest.Rest.Request.Market.GetOrderBookRequest(instrumentId);
            return InvestHandler.HandleGetOrderBookResponse(SendTest(getOrderBookRequest));
        }

        #endregion

        #region [PrivateRest]

        /// <summary>
        /// Тест метода регистрации счёта в песочнице
        /// </summary>
        [Test]
        public void PrivateRestOpenSandboxAccountTest_Success()
        {
            var account = OpenSandboxAccount();

            Assert.IsNotNull(account);
            Assert.IsNotNull(account.AccountId);
        }

        /// <summary>
        /// Тест метод закрытия всех счётов в песочнице
        /// </summary>
        [Test]
        public void PrivateRestCloseAllSandboxAccountsTest_Success()
        {
            var getAccountsResponse = GetAccounts();

            Assert.IsNotNull(getAccountsResponse);
            Assert.IsTrue(getAccountsResponse.Accounts.Count > 0);

            int total_count = getAccountsResponse.Accounts.Count;
            int closed = 0;

            //закрытие всех счетов
            foreach (var account in getAccountsResponse.Accounts)
            {
                var closeAccountResponse =  CloseSandboxAccount(account.Id);

                Assert.IsNotNull(closeAccountResponse);
                closed++;
            }

            if(closed == total_count) Assert.Pass();
            else Assert.Fail();

        }

        /// <summary>
        /// Тест получения списка аккаунтов
        /// </summary>
        [Test]
        public void PrivateRestGetAccountsTest_Success()
        {
            var openAccountResponse = OpenSandboxAccount();

            Assert.IsNotNull(openAccountResponse);
            Assert.IsNotNull(openAccountResponse.AccountId);

            var getAccountsResponse = GetAccounts();

            Assert.IsNotNull(getAccountsResponse);
            Assert.IsTrue(getAccountsResponse.Accounts.Count > 0);

            var account = getAccountsResponse.Accounts.First();
            Assert.IsNotNull(account);
            Assert.IsNotNull(account.Name);
            Assert.IsNotNull(account.Type);
            Assert.IsNotNull(account.AccessLevel);
            Assert.IsNotNull(account.Id);
            Assert.IsNotNull(account.Status);
            Assert.IsNotNull(account.OpenedDate);
        }

        /// <summary>
        /// Метод пополнения счёта в песочнице
        /// </summary>
        [Test]
        public void PrivateRestSandboxPayInTest_Success()
        {
            var openAccountResponse = OpenSandboxAccount();

            Assert.IsNotNull(openAccountResponse);
            Assert.IsNotNull(openAccountResponse.AccountId);

            var sandboxPayInResponse = SandboxPayIn(openAccountResponse.AccountId,  "RUB","1000");

            Assert.IsNotNull(sandboxPayInResponse);
            Assert.IsNotNull(sandboxPayInResponse.Balance);
        }

        /// <summary>
        /// Запрос выставления торгового поручения
        /// </summary>
        [Test]
        public void PrivateRestPostSandboxOrderTest_Success()
        {
            var getInstrumentResponse = GetInstruments();

            Assert.IsNotNull(getInstrumentResponse);
            Assert.IsTrue(getInstrumentResponse.Instruments.Count > 0);

            GetOrderBookResponse currentOrderBook = null;
            InstrumentData currentInstrumentData = null;
            foreach (var instrument in getInstrumentResponse.Instruments)
            {
                var getOrderBookResponse = GetInstrumentOrderBook(instrument.Uid);

                if (getOrderBookResponse.Asks.Count > 0 && getOrderBookResponse.Bids.Count > 0)
                {
                    currentOrderBook = getOrderBookResponse;
                    currentInstrumentData = instrument;
                    break;
                }
            }

            Assert.IsNotNull(currentOrderBook);
            Assert.IsNotNull(currentInstrumentData);

            var firstAsk = currentOrderBook.Asks.FirstOrDefault();
            Assert.IsNotNull(firstAsk);

            var firstBid = currentOrderBook.Bids.FirstOrDefault();
            Assert.IsNotNull(firstBid);

            var openAccountResponse = OpenSandboxAccount();

            Assert.IsNotNull(openAccountResponse);
            Assert.IsNotNull(openAccountResponse.AccountId);

            var sandboxPayInResponse = SandboxPayIn(openAccountResponse.AccountId, "RUB", "1000");
            Assert.IsNotNull(sandboxPayInResponse);

            var postOrderResponse = PostOrder(openAccountResponse.AccountId, currentInstrumentData, (int)firstAsk.Quantity, 
                (int)firstAsk.Price.Units, firstAsk.Price.Nano, OrderDirectionEnum.ORDER_DIRECTION_BUY);

            Assert.IsNotNull(postOrderResponse);
            Assert.IsNotNull(postOrderResponse.Direction);
            Assert.IsNotNull(postOrderResponse.OrderId);
            Assert.IsNotNull(postOrderResponse.ExecutionReportStatus);
            Assert.IsNotNull(postOrderResponse.LotsExecuted);
            Assert.IsNotNull(postOrderResponse.Message);
            Assert.IsNotNull(postOrderResponse.OrderType);
            Assert.IsNotNull(postOrderResponse.DirectionEnum);
            Assert.IsNotNull(postOrderResponse.ExecutionReportStatusEnum);
            Assert.IsNotNull(postOrderResponse.OrderTypeEnum);
            Assert.IsNotNull(postOrderResponse.ExecutedCommission.Units);
            Assert.IsNotNull(postOrderResponse.ExecutedCommission.Nano);
            Assert.IsNotNull(postOrderResponse.ExecutedCommission.Currency);
            Assert.IsNotNull(postOrderResponse.ExecutedOrderPrice.Units);
            Assert.IsNotNull(postOrderResponse.ExecutedOrderPrice.Currency);
            Assert.IsNotNull(postOrderResponse.ExecutedOrderPrice.Nano);
            Assert.IsNotNull(postOrderResponse.InitialCommission.Units);
            Assert.IsNotNull(postOrderResponse.InitialCommission.Currency);
            Assert.IsNotNull(postOrderResponse.InitialCommission.Nano);
            Assert.IsNotNull(postOrderResponse.InitialOrderPrice.Units);
            Assert.IsNotNull(postOrderResponse.InitialOrderPrice.Currency);
            Assert.IsNotNull(postOrderResponse.InitialOrderPrice.Nano);
            Assert.IsNotNull(postOrderResponse.TotalOrderAmount.Units);
            Assert.IsNotNull(postOrderResponse.TotalOrderAmount.Currency);
            Assert.IsNotNull(postOrderResponse.TotalOrderAmount.Nano);
        }

        /// <summary>
        /// Запрос отмены торгового поручения
        /// </summary>
        [Test]
        public void PrivateRestCancelOrderOrderTest_Success()
        {
            var getInstrumentResponse = GetInstruments();

            Assert.IsNotNull(getInstrumentResponse);
            Assert.IsTrue(getInstrumentResponse.Instruments.Count > 0);

            GetOrderBookResponse currentOrderBook = null;
            InstrumentData currentInstrumentData = null;
            foreach (var instrument in getInstrumentResponse.Instruments)
            {
                var getOrderBookResponse = GetInstrumentOrderBook(instrument.Uid);

                if (getOrderBookResponse.Asks.Count > 0 && getOrderBookResponse.Bids.Count > 0)
                {
                    currentOrderBook = getOrderBookResponse;
                    currentInstrumentData = instrument;
                    break;
                }
            }

            Assert.IsNotNull(currentOrderBook);
            Assert.IsNotNull(currentInstrumentData);

            var firstAsk = currentOrderBook.Asks.FirstOrDefault();
            Assert.IsNotNull(firstAsk);

            var firstBid = currentOrderBook.Bids.FirstOrDefault();
            Assert.IsNotNull(firstBid);

            var openAccountResponse = OpenSandboxAccount();

            Assert.IsNotNull(openAccountResponse);
            Assert.IsNotNull(openAccountResponse.AccountId);

            var sandboxPayInResponse = SandboxPayIn(openAccountResponse.AccountId, "RUB", "1000");
            Assert.IsNotNull(sandboxPayInResponse);

            var postOrderResponse = PostOrder(openAccountResponse.AccountId, currentInstrumentData, (int)firstAsk.Quantity,
                (int)firstAsk.Price.Units, firstAsk.Price.Nano, OrderDirectionEnum.ORDER_DIRECTION_BUY);

            Assert.IsNotNull(postOrderResponse);
            Assert.IsNotNull(postOrderResponse.Direction);
            Assert.IsNotNull(postOrderResponse.OrderId);
            Assert.IsNotNull(postOrderResponse.ExecutionReportStatus);
            Assert.IsNotNull(postOrderResponse.LotsExecuted);
            Assert.IsNotNull(postOrderResponse.Message);
            Assert.IsNotNull(postOrderResponse.OrderType);
            Assert.IsNotNull(postOrderResponse.DirectionEnum);
            Assert.IsNotNull(postOrderResponse.ExecutionReportStatusEnum);
            Assert.IsNotNull(postOrderResponse.OrderTypeEnum);
            Assert.IsNotNull(postOrderResponse.ExecutedCommission.Units);
            Assert.IsNotNull(postOrderResponse.ExecutedCommission.Nano);
            Assert.IsNotNull(postOrderResponse.ExecutedCommission.Currency);
            Assert.IsNotNull(postOrderResponse.ExecutedOrderPrice.Units);
            Assert.IsNotNull(postOrderResponse.ExecutedOrderPrice.Currency);
            Assert.IsNotNull(postOrderResponse.ExecutedOrderPrice.Nano);
            Assert.IsNotNull(postOrderResponse.InitialCommission.Units);
            Assert.IsNotNull(postOrderResponse.InitialCommission.Currency);
            Assert.IsNotNull(postOrderResponse.InitialCommission.Nano);
            Assert.IsNotNull(postOrderResponse.InitialOrderPrice.Units);
            Assert.IsNotNull(postOrderResponse.InitialOrderPrice.Currency);
            Assert.IsNotNull(postOrderResponse.InitialOrderPrice.Nano);
            Assert.IsNotNull(postOrderResponse.TotalOrderAmount.Units);
            Assert.IsNotNull(postOrderResponse.TotalOrderAmount.Currency);
            Assert.IsNotNull(postOrderResponse.TotalOrderAmount.Nano);

            var cancelOrderRequest = new TinkoffMapper.Invest.Rest.Request.Account.CancelOrderRequest(openAccountResponse.AccountId, postOrderResponse.OrderId);
            var cancelOrderResponse = InvestHandler.HandleCancelOrderResponse(SendTest(cancelOrderRequest));

            Assert.IsNotNull(cancelOrderResponse);
            Assert.IsNotNull(cancelOrderResponse.Time);
        }

        /// <summary>
        /// Запрос позиций портфеля по счёту
        /// </summary>
        [Test]
        public void PrivateRestPositionsTest_Success()
        {
            var openAccountsRequest = new OpenSandboxAccountRequest();
            var openAccountsResponse = SendTest(null, openAccountsRequest);

            Assert.IsNotNull(openAccountsResponse);

            var getAccountsRequest = new GetAccountsRequest();
            var getAccountsResponse = InvestHandler.HandleGetAccountsResponse(SendTest(getAccountsRequest));

            Assert.IsNotNull(getAccountsResponse);
            Assert.IsTrue(getAccountsResponse.Accounts.Count > 0);

            var account = getAccountsResponse.Accounts.First();

            var positionsRequest = new TinkoffMapper.Invest.Rest.Request.Account.PositionsRequest(account.Id);
            var positionsResponse = InvestHandler.HandlePositionsResponse(SendTest(positionsRequest));

            Assert.IsNotNull(positionsResponse);
        }

        /// <summary>
        /// Запрос получения списка операций по счёту
        /// </summary>
        [Test]
        public void PrivateRestOperationsTest_Success()
        {
            var getAccountsRequest = new GetAccountsRequest();
            var getAccountsResponse = InvestHandler.HandleGetAccountsResponse(SendTest(getAccountsRequest));

            Assert.IsNotNull(getAccountsResponse);
            Assert.IsTrue(getAccountsResponse.Accounts.Count > 0);

            var account = getAccountsResponse.Accounts.First();

            var operationsRequest = new OperationsRequest(account.Id, Start, End, OperationStateEnum.OPERATION_STATE_EXECUTED, Figi);
            var operationsResponse = InvestHandler.HandleOperationsResponse(SendTest(operationsRequest));

            Assert.IsNotNull(operationsResponse);
        }

        /// <summary>
        /// Запрос получения текущего портфеля по счёту
        /// </summary>
        [Test]
        public void PrivateRestPortfolioTest_Success()
        {
            var getAccountsRequest = new GetAccountsRequest();
            var getAccountsResponse = InvestHandler.HandleGetAccountsResponse(SendTest(getAccountsRequest));

            Assert.IsNotNull(getAccountsResponse);
            Assert.IsTrue(getAccountsResponse.Accounts.Count > 0);

            var account = getAccountsResponse.Accounts.First();

            var getPortfolioRequest = new GetPortfolioRequest(account.Id, "RUB");
            var getPortfolioResponse = InvestHandler.HandleGetPortfolioResponse(SendTest(getPortfolioRequest));

            Assert.IsNotNull(getPortfolioResponse);
            Assert.IsNotNull(getPortfolioResponse.TotalAmountBonds);
            Assert.IsNotNull(getPortfolioResponse.TotalAmountCurrencies);
            Assert.IsNotNull(getPortfolioResponse.TotalAmountEtf);
            Assert.IsNotNull(getPortfolioResponse.TotalAmountFutures);
            Assert.IsNotNull(getPortfolioResponse.TotalAmountShares);
        }


        /// <summary>
        /// Метод регистрации счёта в песочнице
        /// </summary>
        private OpenSandboxAccountResponse OpenSandboxAccount()
        {
            var openAccountsRequest = new OpenSandboxAccountRequest();
            var openAccountsResponse = SendTest(null, openAccountsRequest);
            return Parse<OpenSandboxAccountResponse>(openAccountsResponse);
        }

        /// <summary>
        /// Список счетов пользователя
        /// </summary>
        /// <returns></returns>
        private GetAccountsResponse GetAccounts()
        {
            var getAccountsRequest = new GetAccountsRequest();
            return InvestHandler.HandleGetAccountsResponse(SendTest(getAccountsRequest));
        }

        /// <summary>
        /// Метод закрытия счёта в песочнице
        /// </summary>
        /// <param name="accountId">Ид аккаунта</param>
        /// <returns></returns>
        private CloseSandboxAccountResponse CloseSandboxAccount(string accountId)
        {
            var closeAccountRequest = new CloseSandboxAccountRequest(accountId);
            var closeAccountResponse = SendTest(null, closeAccountRequest);
            return Parse<CloseSandboxAccountResponse>(closeAccountResponse);
        }

        /// <summary>
        /// Метод пополнения счёта в песочнице
        /// </summary>
        /// <param name="accountId">Ид аккаунта</param>
        /// <param name="currency">Строковый ISO-код валюты</param>
        /// <param name="units">Целая часть суммы</param>
        /// <param name="nano">Дробная часть суммы, может быть отрицательным числом</param>
        /// <returns></returns>
        private SandboxPayInResponse SandboxPayIn(string accountId, string currency, string units, int nano = 0)
        {
            var sandboxPayInRequest = new SandboxPayInRequest(accountId, new Amount() { currency = currency, units = units, nano = nano });
            var sandboxPayInResponse = SendTest(null, sandboxPayInRequest);
            return Parse<SandboxPayInResponse>(sandboxPayInResponse);
        }

        /// <summary>
        /// Запрос выставления торгового поручения
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="figi">Deprecated Figi-идентификатор инструмента. Необходимо использовать instrument_id.</param>
        /// <param name="instrument_id">Идентификатор инструмента, принимает значения Figi или Instrument_uid.</param>
        /// <param name="quantity"></param>
        /// <param name="units">Целая часть суммы, может быть отрицательным числом</param>
        /// <param name="nano">Дробная часть суммы, может быть отрицательным числом</param>
        /// <param name="orderDirection"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        private PostOrderResponse PostOrder(string accountId, InstrumentData instrument, int quantity, int units, int nano, 
            OrderDirectionEnum orderDirection, OrderTypeEnum orderType = OrderTypeEnum.ORDER_TYPE_LIMIT)
        {
            var postOrderRequest = new TinkoffMapper.Invest.Rest.Request.Account.PostOrderRequest(instrument.Figi, instrument.Uid, quantity, new QuotationData(units, nano), orderDirection, accountId, orderType, "");
            return InvestHandler.HandlePostOrderResponse(SendTest(postOrderRequest));
        }

        #endregion

        /// <summary>
        /// Отправка рест-запроса
        /// </summary>
        /// <param name="investRequest">Параметры</param>
        /// <param name="sandboxRequest">Параметры для печоницы</param>
        /// <returns></returns>
        public string SendTest(RequestInvest investRequest, SandboxRequestInvest sandboxRequest = null)
        {
            var request = sandboxRequest == null ? requestArranger.Arrange(investRequest) : sandboxRequestArranger.Arrange(sandboxRequest);
            var req = new RestRequest(request.Query, MapRequestMethod(request.Method));

            req.AddHeader("Authorization", $"Bearer {ApiToken}");
            req.AddHeader("Content-Type", "application/json");
            req.AddHeader("Accept", "application/json");

            req.RequestFormat = DataFormat.Json;

            if (request.Body != null && !string.IsNullOrEmpty(request.Body.ToString()))
            {
                req.AddJsonBody(request.Body);
            }
            else
            {
                req.AddJsonBody(new {});
            }

            var result = client.Execute(req);
            
            return (result.Content);
        }

        private static Method MapRequestMethod(RequestMethod method)
        {
            switch (method)
            {
                case RequestMethod.GET:
                    return Method.Get;
                case RequestMethod.POST:
                    return Method.Post;
                case RequestMethod.PUT:
                    return Method.Put;
                case RequestMethod.DELETE:
                    return Method.Delete;
                default:
                    throw new NotImplementedException();
            }
        }

        private static T Parse<T>(string message)
        {
            return JsonConvert.DeserializeObject<T>(message, new StringEnumConverter(),
                new ProtoMessageConverter());
        }
    }
}

