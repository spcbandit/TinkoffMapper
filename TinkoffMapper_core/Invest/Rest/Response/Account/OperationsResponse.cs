using System.Collections.Generic;
using Newtonsoft.Json;
using TinkoffMapper.Invest.Rest.Data.Account;

namespace TinkoffMapper.Invest.Rest.Response.Account
{
    /// <summary>
    /// Список операций.
    /// </summary>
    public sealed class OperationsResponse
    {
        [JsonConstructor]
        public OperationsResponse(ICollection<OperationData> operations)
        {
            Operations = operations;
        }

        [JsonProperty("operations")]
        public ICollection<OperationData> Operations { get; set; }
    }
}
