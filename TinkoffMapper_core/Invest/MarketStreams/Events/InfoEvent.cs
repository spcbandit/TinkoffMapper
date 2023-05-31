using Tinkoff.Proto.InvestApi.V1;


namespace TinkoffMapper.Invest.MarketStreams.Events
{
    /// <summary>
    /// Евент подписки на торговые статусы инструментов
    /// </summary>
    public class InfoEvent
    {
        public InfoEvent(TradingStatus status)
        {
            Status = status;
        }
        /// <summary>
        /// Пакет изменения торгового статуса
        /// </summary>
        public TradingStatus Status { get; set; }
    }
}
