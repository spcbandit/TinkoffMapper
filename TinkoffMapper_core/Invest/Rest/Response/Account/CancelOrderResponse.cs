using System;
using Newtonsoft.Json;

namespace TinkoffMapper.Invest.Rest.Response.Account
{
    /// <summary>
    /// Результат отмены торгового поручения.
    /// </summary>
    public sealed class CancelOrderResponse
    {
        [JsonConstructor]
        public CancelOrderResponse(DateTimeOffset time)
        {
            Time = time;
        }

        [JsonProperty("time")]
        public DateTimeOffset Time { get; set; }
    }
}
