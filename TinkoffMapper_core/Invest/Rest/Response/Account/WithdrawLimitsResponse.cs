using System.Collections.Generic;
using Newtonsoft.Json;
using TinkoffMapper.Invest.Rest.Data.Market;

namespace TinkoffMapper.Invest.Rest.Response.Account
{
    /// <summary>
    /// Доступный для вывода остаток.
    /// </summary>
    public sealed class WithdrawLimitsResponse
    {
        [JsonConstructor]
        public WithdrawLimitsResponse(ICollection<MoneyValueData> money, ICollection<MoneyValueData> blocked, 
            ICollection<MoneyValueData> blockedGuarantee)
        {
            Money = money;
            Blocked = blocked;
            BlockedGuarantee = blockedGuarantee;
        }

        [JsonProperty("money")]
        public ICollection<MoneyValueData> Money { get; set; }

        [JsonProperty("blocked")]
        public ICollection<MoneyValueData> Blocked { get; set; }

        [JsonProperty("blockedGuarantee")]
        public ICollection<MoneyValueData> BlockedGuarantee { get; set; }
    }
}
