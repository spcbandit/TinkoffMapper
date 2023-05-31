using Newtonsoft.Json;

namespace TinkoffMapper.Invest.Rest.Common.Data.Account
{
    /// <summary>
    /// Котировка - денежная сумма без указания валюты
    /// </summary>
    public sealed class QuotationData
    {
        [JsonConstructor]
        public QuotationData(long? units, int? nano)
        {
            Units = units;
            Nano = nano;
        }

        [JsonProperty("units")]
        public long? Units { get; set; }
        /// <summary>
        /// В nano указывается в 10 минус 9 степени. 7362000 / 1_000_000_000 
        /// (пишу с андерскорами чтобы показать количество нулей) = 0,007362
        /// </summary>

        [JsonProperty("nano")]
        public int? Nano { get; set; }
    }
}
