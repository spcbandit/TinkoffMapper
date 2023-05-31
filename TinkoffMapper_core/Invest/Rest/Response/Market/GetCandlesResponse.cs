using System.Collections.Generic;
using Newtonsoft.Json;
using TinkoffMapper.Invest.Rest.Data.Market;

namespace TinkoffMapper.Invest.Rest.Response.Market
{
    /// <summary>
    /// Список свечей.
    /// </summary>
    public sealed class GetCandlesResponse
    {
        [JsonConstructor]
        public GetCandlesResponse(ICollection<HistoricCandleData> candles)
        {
            Candles = candles;
        }

        [JsonProperty("candles")]
        public ICollection<HistoricCandleData> Candles { get; set; }
    }
}
