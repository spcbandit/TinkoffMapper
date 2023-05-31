using System.Collections.Generic;
using Newtonsoft.Json;
using TinkoffMapper.Invest.Rest.Data.Market;

namespace TinkoffMapper.Invest.Rest.Response.Market
{
    /// <summary>
    /// Список валют.
    /// </summary>
    public sealed class InstrumentResponse
    {
        [JsonConstructor]
        public InstrumentResponse(ICollection<InstrumentData> instrument)
        {
            Instruments = instrument;
        }

        [JsonProperty("instruments")]
        public ICollection<InstrumentData> Instruments { get; set; }

    }
}
