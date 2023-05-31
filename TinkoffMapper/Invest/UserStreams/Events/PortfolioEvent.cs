using Tinkoff.Proto.InvestApi.V1;

namespace TinkoffMapper.Invest.UserStreams.Events
{
    /// <summary>
    /// Евент обновления портфеля
    /// </summary>
    public class PortfolioEvent
    {
        public PortfolioEvent(PortfolioResponse portfolio)
        {
            Portfolio = portfolio;
        }

        /// <summary>
        /// Текущий портфель по счёту
        /// </summary>
        public PortfolioResponse Portfolio { get; set; }
    }
}
