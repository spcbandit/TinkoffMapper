using System.Collections.Generic;
using Newtonsoft.Json;
using TinkoffMapper.Invest.Rest.Data.Account;
using TinkoffMapper.Invest.Rest.Data.Market;

namespace TinkoffMapper.Invest.Rest.Response.Account
{
    /// <summary>
    /// Список позиций по счёту.
    /// </summary>
    public sealed class PositionsResponse
    {
        [JsonConstructor]
        public PositionsResponse(ICollection<MoneyValueData> money, ICollection<MoneyValueData> blocked, 
            ICollection<PositionsSecuritiesData> securities, bool limitsLoadingInProgress, 
            ICollection<PositionsFuturesData> futures)
        {
            Money = money;
            Blocked = blocked;
            Securities = securities;
            LimitsLoadingInProgress = limitsLoadingInProgress;
            Futures = futures;
        }

        [JsonProperty("money")]
        public ICollection<MoneyValueData> Money { get; set; }

        [JsonProperty("blocked")]
        public ICollection<MoneyValueData> Blocked { get; set; }

        [JsonProperty("securities")]
        public ICollection<PositionsSecuritiesData> Securities { get; set; }

        [JsonProperty("limitsLoadingInProgress")]
        public bool LimitsLoadingInProgress { get; set; }

        [JsonProperty("futures")]
        public ICollection<PositionsFuturesData> Futures { get; set; }
    }
}
