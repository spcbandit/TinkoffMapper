using Tinkoff.Proto.InvestApi.V1;

namespace TinkoffMapper.Invest.MarketStreams.Events
{
    /// <summary>
    /// Евент подписки на свечи
    /// </summary>
    public class CandlesHistoryEvent
    {
        public CandlesHistoryEvent(Candle candle)
        {
            Candle = candle;
        }

        /// <summary>
        /// Пакет свечей в рамках стрима
        /// </summary>
        public Candle Candle { get; set; }
    }
}
