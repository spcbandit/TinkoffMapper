using TinkoffMapper.Extensions;
using TinkoffMapper.Invest.UserStreams.Events;

namespace TinkoffMapper.Invest.UserStreams
{
    /// <summary>
    /// Композиция
    /// </summary>
    public sealed class ContractsUserStreamsComposition
    {
        /// <summary>
        /// Обновления портфеля
        /// </summary>
        public PortfolioEvent HandlePortfolioResponse(string json) => json.Deserialize<PortfolioEvent>();
        /// <summary>
        /// Изменения позиций портфеля
        /// </summary>
        public PositionsEvent HandlePositionsResponse(string json) => json.Deserialize<PositionsEvent>();
        /// <summary>
        /// Информация об исполнении торгового поручения
        /// </summary>
        public TradesEvent HandleTradesResponse(string json) => json.Deserialize<TradesEvent>();
        /// <summary>
        /// Проверка активности стрима.
        /// </summary>
        public PingEvent HandlePingEvent(string json) => json.Deserialize<PingEvent>();
    }
}
