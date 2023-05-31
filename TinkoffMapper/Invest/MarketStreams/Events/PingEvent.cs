
using Tinkoff.Proto.InvestApi.V1;

namespace TinkoffMapper.Invest.MarketStreams.Events
{
    /// <summary>
    /// Евент проверки активности стрима
    /// </summary>
    public class PingEvent
    {
        public PingEvent(Ping ping)
        {
            Ping = ping;
        }
        /// <summary>
        /// Пинг стрима
        /// </summary>
        public Ping Ping { get; set; }
    }
}
