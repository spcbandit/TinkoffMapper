using TinkoffMapper.Extensions;
using TinkoffMapper.Invest.MarketStreams.Events;
namespace TinkoffMapper.Invest.MarketStreams
{
    /// <summary>
    /// Композиция
    /// </summary>
    public sealed class ContractsMarketStreamsComposition
    {
        /// <summary>
        /// Свечи
        /// </summary>
        public CandlesHistoryEvent HandleCandlesHistoryResponse(string json) => json.Deserialize<CandlesHistoryEvent>();
        /// <summary>
        /// Торговые статусы инструментов
        /// </summary>
        public InfoEvent HandleInfoResponse(string json) => json.Deserialize<InfoEvent>();
        /// <summary>
        /// Цены последних сделок
        /// </summary>
        public LastPriceEvent HandleLastPriceResponse(string json) => json.Deserialize<LastPriceEvent>();
        /// <summary>
        /// Стаканы
        /// </summary>
        public OrderBookEvent HandleOrderBookResponse(string json) => json.Deserialize<OrderBookEvent>();
        /// <summary>
        /// Лента обезличенных сделок
        /// </summary>
        public TradesEvent HandleTradesEvent(string json) => json.Deserialize<TradesEvent>();
        /// <summary>
        /// Проверка активности стрима.
        /// </summary>
        public PingEvent HandlePingEvent(string json) => json.Deserialize<PingEvent>();
    }
}
