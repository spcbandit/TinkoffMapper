using Tinkoff.Proto.InvestApi.V1;


namespace TinkoffMapper.Invest.MarketStreams.Events
{
    /// <summary>
    /// Евент подписки на цены последних сделок
    /// </summary>
    public class LastPriceEvent
    {
        public LastPriceEvent(LastPrice lastPrice)
        {
            LastPrice = lastPrice;
        }

        /// <summary>
        /// Информация о цене последней сделки
        /// </summary>
        public LastPrice LastPrice { get; set; }
    }
}
