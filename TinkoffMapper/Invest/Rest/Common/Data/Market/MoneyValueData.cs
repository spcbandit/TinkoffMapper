using Newtonsoft.Json;

namespace TinkoffMapper.Invest.Rest.Common.Data.Market
{
    /// <summary>
    /// Денежная сумма в определенной валюте
    /// </summary>
    public sealed class MoneyValueData
    {
        [JsonConstructor]
        public MoneyValueData(string currency, string units, int nano)
        {
            Currency = currency;
            Units = units;
            Nano = nano;
        }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }

        [JsonProperty("nano")]
        public int Nano { get; set; }

    }
}
