using Newtonsoft.Json;

namespace TinkoffMapper.Invest.Rest.Data.Account
{
    /// <summary>
    /// Баланс фьючерса.
    /// </summary>
    public sealed class PositionsFuturesData
    {
        [JsonConstructor]
        public PositionsFuturesData(string figi, string blocked, string balance)
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
