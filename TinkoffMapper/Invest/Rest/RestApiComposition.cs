using Tinkoff.Proto.InvestApi.V1;
using TinkoffMapper.Extensions;
using TinkoffMapper.Invest.Rest.Common.Response;
using CancelOrderResponse = Tinkoff.Proto.InvestApi.V1.CancelOrderResponse;
using GetAccountsResponse = Tinkoff.Proto.InvestApi.V1.GetAccountsResponse;
using OperationsResponse = Tinkoff.Proto.InvestApi.V1.OperationsResponse;
using PositionsResponse = Tinkoff.Proto.InvestApi.V1.PositionsResponse;
using PostOrderResponse = Tinkoff.Proto.InvestApi.V1.PostOrderResponse;
using WithdrawLimitsResponse = Tinkoff.Proto.InvestApi.V1.WithdrawLimitsResponse;


namespace TinkoffMapper.Invest.Rest
{
    public class RestApiComposition
    {
        #region [RestMarket]
        /// <summary>
        /// Получение исторических свечей по инструменту
        /// </summary>
        public GetCandlesResponse HandleGetCandlesResponse(string json) => json.Deserialize<GetCandlesResponse>();

        /// <summary>
        /// Получение списка фьючерсов
        /// </summary>
        public FuturesResponse HandleFuturesInstrumentsResponse(string json) => json.Deserialize<FuturesResponse>();
        
        /// <summary>
        /// Получение фьючерса по его идентификатору
        /// </summary>
        public FutureResponse HandleFutureByResponse(string json) => json.Deserialize<FutureResponse>();
        
        /// <summary>
        /// Получение списка акций
        /// </summary>
        public SharesResponse HandleSharesInstrumentsResponse(string json) => json.Deserialize<SharesResponse>();

        /// <summary>
        /// Получение акции по его идентификатору
        /// </summary>
        public ShareResponse HandleShareByResponse(string json) => json.Deserialize<ShareResponse>();
        
        /// <summary>
        /// Получение списка валют
        /// </summary>
        public CurrenciesResponse HandleCurrenciesInstrumentsResponse(string json) => json.Deserialize<CurrenciesResponse>();

        /// <summary>
        /// Получение валюты по его идентификатору
        /// </summary>
        public CurrencyResponse HandleCurrencyByResponse(string json) => json.Deserialize<CurrencyResponse>();
        
        /// <summary>
        /// Получение облигаций
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public BondsResponse HandleBondsResponse(string json) => json.Deserialize<BondsResponse>();
        
        /// <summary>
        /// Запрос получения инструмента по идентификатору
        /// </summary>
        public Instrument HandleGetInstrumentByResponse(string json) => json.Deserialize<Instrument>();

        /// <summary>
        /// Получение цен последних сделок по инструментам.
        /// </summary>
        public GetLastPricesResponse HandleGetLastPricesResponse(string json) => json.Deserialize<GetLastPricesResponse>();

        /// <summary>
        /// Получение стакана по инструменту
        /// </summary>
        public GetOrderBookResponse HandleGetOrderBookResponse(string json) => json.Deserialize<GetOrderBookResponse>();

        #endregion
        #region [RestAccount]

        /// <summary>
        /// Получение отмены торгового поручения
        /// </summary>
        public CancelOrderResponse HandleCancelOrderResponse(string json) => json.Deserialize<CancelOrderResponse>();

        /// <summary>
        /// Получение списка операций по счёту
        /// </summary>
        public OperationsResponse HandleOperationsResponse(string json) => json.Deserialize<OperationsResponse>();

        /// <summary>
        /// Получение позиций портфеля по счёту
        /// </summary>
        public PositionsResponse HandlePositionsResponse(string json) => json.Deserialize<PositionsResponse>();

        /// <summary>
        /// Получение ответа на выставление торгового поручения
        /// </summary>
        public PostOrderResponse HandlePostOrderResponse(string json) => json.Deserialize<PostOrderResponse>();

        /// <summary>
        /// Получение доступного для вывода остатка
        /// </summary>
        public WithdrawLimitsResponse HandleWithdrawLimitsResponse(string json) => json.Deserialize<WithdrawLimitsResponse>();

        /// <summary>
        /// Получение cписка счетов пользователя
        /// </summary>
        public GetAccountsResponse HandleGetAccountsResponse(string json) => json.Deserialize<GetAccountsResponse>();

        /// <summary>
        /// Получение текущего портфеля по счёту
        /// </summary>
        public PortfolioResponse HandleGetPortfolioResponse(string json) => json.Deserialize<PortfolioResponse>();

        /// <summary>
        /// Получение списка активных заявок по счёту
        /// </summary>
        public GetOrdersResponse HandleGetOrdersResponse(string json) => json.Deserialize<GetOrdersResponse>();


        /// <summary>
        /// Метод получения статуса торгового поручения
        /// </summary>
        public OrderState HandleGetOrderStateResponse(string json) => json.Deserialize<OrderState>();

        #endregion

        /// <summary>
        /// Получение ошибки
        /// </summary>
        public ErrorMessage HandleErrorResponse(string json) => json.Deserialize<ErrorMessage>();
    }
}
