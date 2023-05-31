using Tinkoff.Proto.InvestApi.V1;

namespace TinkoffMapper.Invest.UserStreams.Events
{
    /// <summary>
    /// Евент обновлений информации по изменению позиций портфеля
    /// </summary>
    public class PositionsEvent
    {
        public PositionsEvent(PositionData position)
        {
            Position = position;
        }

        /// <summary>
        /// Данные о позиции портфеля
        /// </summary>
        public PositionData Position { get; set; }
    }
}
