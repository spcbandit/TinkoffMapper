using Tinkoff.Proto.InvestApi.V1;


namespace TinkoffMapper.Invest.MarketStreams.Events
{
    /// <summary>
    /// Евент подписки на ленту обезличенных сделок
    /// </summary>
    public class TradesEvent
    {
        public TradesEvent(Trade trade)
        {
            Trade = trade;
        }

        /// <summary>
        /// Информация о сделке
        /// </summary>
        public Trade Trade { get; set; }
    }
}
