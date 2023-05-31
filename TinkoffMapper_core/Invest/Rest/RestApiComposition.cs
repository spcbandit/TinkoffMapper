using Tinkoff.Proto.InvestApi.V1;
using TinkoffMapper.Extensions;
using TinkoffMapper.Invest.Rest.Response.Account;
using CancelOrderResponse = TinkoffMapper.Invest.Rest.Response.Account.CancelOrderResponse;
using GetAccountsResponse = TinkoffMapper.Invest.Rest.Response.Account.GetAccountsResponse;
using GetCandlesResponse = TinkoffMapper.Invest.Rest.Response.Market.GetCandlesResponse;
using GetLastPricesResponse = TinkoffMapper.Invest.Rest.Response.Market.GetLastPricesResponse;
using InstrumentResponse = TinkoffMapper.Invest.Rest.Response.Market.InstrumentResponse;
using OperationsResponse = Tinkoff.Proto.InvestApi.V1.OperationsResponse;
using PositionsResponse = TinkoffMapper.Invest.Rest.Response.Account.PositionsResponse;
using PostOrderResponse = TinkoffMapper.Invest.Rest.Response.Account.PostOrderResponse;
using WithdrawLimitsResponse = TinkoffMapper.Invest.Rest.Response.Account.WithdrawLimitsResponse;


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
        /// Получение списка инструментов
        /// </summary>
        public InstrumentResponse HandleInstrumentResponse(string json) => json.Deserialize<InstrumentResponse>();

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
        public GetPortfolioResponse HandleGetPortfolioResponse(string json) => json.Deserialize<GetPortfolioResponse>();
        #endregion
    }
}
