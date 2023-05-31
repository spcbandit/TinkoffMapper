using System.Collections.Generic;
using Newtonsoft.Json;
using Tinkoff.Proto.InvestApi.V1;

namespace TinkoffMapper.Invest.Rest.Response.Account
{
    /// <summary>
    /// Позиции портфеля
    /// </summary>
    public sealed class GetPortfolioResponse
    {
        [JsonConstructor]
        public GetPortfolioResponse(MoneyValue totalAmountShares, MoneyValue totalAmountBonds, MoneyValue totalAmountEtf, 
            MoneyValue totalAmountCurrencies, MoneyValue totalAmountFutures, ICollection<PortfolioPosition> positions, 
            string accountId, ICollection<VirtualPortfolioPosition> virtualPositions)
        {
            TotalAmountShares = totalAmountShares;
            TotalAmountBonds = totalAmountBonds;
            TotalAmountEtf = totalAmountEtf;
            TotalAmountCurrencies = totalAmountCurrencies;
            TotalAmountFutures = totalAmountFutures;
            Positions = positions;
            AccountId = accountId;
            VirtualPositions = virtualPositions;
        }

        [JsonProperty("total_amount_shares")]
        public MoneyValue TotalAmountShares { get; set; }

        [JsonProperty("total_amount_bonds")]
        public MoneyValue TotalAmountBonds { get; set; }

        [JsonProperty("total_amount_etf")]
        public MoneyValue TotalAmountEtf { get; set; }

        [JsonProperty("total_amount_currencies")]
        public MoneyValue TotalAmountCurrencies { get; set; }

        [JsonProperty("total_amount_futures")]
        public MoneyValue TotalAmountFutures { get; set; }

        [JsonProperty("expected_yield")]
        public Quotation ExpectedYield { get; set; }

        [JsonProperty("positions")]
        public ICollection<PortfolioPosition> Positions { get; set; }

        [JsonProperty("accountId")]
        public string AccountId { get; set; }

        [JsonProperty("total_amount_options")]
        public string TotalAmountOptions { get; set; }

        [JsonProperty("total_amount_sp")]
        public string TotalAmountSp { get; set; }

        [JsonProperty("total_amount_portfolio")]
        public string TotalAmountPortfolio { get; set; }

        [JsonProperty("virtualPositions")]
        public ICollection<VirtualPortfolioPosition> VirtualPositions { get; set; }
    }
}
