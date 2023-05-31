using Newtonsoft.Json;

namespace TinkoffMapper.Invest.Rest.Data.Account
{
    /// <summary>
    /// Котировка - денежная сумма без указания валюты
    /// </summary>
    public sealed class QuotationData
    {
        [JsonConstructor]
        public QuotationData(int? nano, string units, string currency)
        {
            Nano = nano;
            Units = units;
            Currency = currency;
        }

        [JsonProperty("nano")]
        public int? Nano { get; set; }

        [JsonProperty("units")]
        public string Units { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }
    }
}
