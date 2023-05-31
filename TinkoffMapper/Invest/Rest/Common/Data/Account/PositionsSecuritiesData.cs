using Newtonsoft.Json;

namespace TinkoffMapper.Invest.Rest.Common.Data.Account
{
    /// <summary>
    /// Баланс позиции ценной бумаги.
    /// </summary>
    public sealed class PositionsSecuritiesData
    {
        [JsonConstructor]
        public PositionsSecuritiesData(string figi, string blocked, string balance)
        {
            Figi = figi;
            Blocked = blocked;
            Balance = balance;
        }

        [JsonProperty("figi")]
        public string Figi { get; set; }

        [JsonProperty("blocked")]
        public string Blocked { get; set; }

        [JsonProperty("balance")]
        public string Balance { get; set; }

    }
}
