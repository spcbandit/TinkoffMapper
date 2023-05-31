using System.Collections.Generic;
using Newtonsoft.Json;

namespace TinkoffMapper.Invest.Rest.Response.Market
{
    /// <summary>
    /// Список цен последних сделок
    /// </summary>
    public sealed class GetLastPricesResponse
    {
        [JsonConstructor]
        public GetLastPricesResponse(ICollection<object> prices)
        {
            Prices = prices;
        }

        [JsonProperty("lastPrices")]
        public ICollection<object> Prices { get; set; }
    }
}
