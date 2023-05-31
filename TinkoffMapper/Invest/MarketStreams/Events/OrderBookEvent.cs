using Tinkoff.Proto.InvestApi.V1;


namespace TinkoffMapper.Invest.MarketStreams.Events
{
    /// <summary>
    /// Евент подписки на стаканы
    /// </summary>
    public class OrderBookEvent
    {
        public OrderBookEvent(OrderBook orderBook)
        {
            OrderBook = orderBook;
        }
        /// <summary>
        /// Пакет стаканов в рамках стрима
        /// </summary>
        public OrderBook OrderBook { get; set; }
    }
}
