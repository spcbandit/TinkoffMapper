using System.Collections.Generic;
using Newtonsoft.Json;

namespace TinkoffMapper.Invest.Rest.Response.Account
{
    /// <summary>
    /// Список счетов пользователя
    /// </summary>
    public sealed class GetAccountsResponse
    {
        [JsonConstructor]
        public GetAccountsResponse(ICollection<Tinkoff.Proto.InvestApi.V1.Account> accounts)
        {
            Accounts = accounts;
        }

        [JsonProperty("accounts")]
        public ICollection<Tinkoff.Proto.InvestApi.V1.Account> Accounts { get; set; }
    }
}
