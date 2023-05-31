using Tinkoff.Proto.InvestApi.V1;

namespace TinkoffMapper.Invest.UserStreams.Events
{
    /// <summary>
    /// Евент по сделокам пользователя
    /// </summary>
    public class TradesEvent
    {
        public TradesEvent(OrderTrades trades)
        {
            Trades = trades;
        }

        /// <summary>
        /// Информация об исполнении торгового поручения
        /// </summary>
        public OrderTrades Trades { get; set; }
    }
}
