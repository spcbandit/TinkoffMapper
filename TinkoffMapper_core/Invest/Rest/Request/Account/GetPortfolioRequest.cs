using System.Collections.Generic;
using TinkoffMapper.Extensions;
using TinkoffMapper.Requests;

namespace TinkoffMapper.Invest.Rest.Request.Account
{
    public class GetPortfolioRequest : RequestInvest
    {
        public GetPortfolioRequest(string accountId, string currency)
        {
            AccountId = accountId;
            Currency = currency;
        }
        public string AccountId { get; set; }
        public string Currency { get; set; }
        internal override string EndPoint => "/tinkoff.public.invest.api.contract.v1.OperationsService/GetPortfolio";
        internal override string SandboxEndPoint => "tinkoff.public.invest.api.contract.v1.SandboxService/GetSandboxPortfolio";

        internal override RequestMethod Method => RequestMethod.POST;
        internal override object Body
        {
            get
            {
                var res = new Dictionary<string, string>();

                res.AddStringIfNotEmptyOrWhiteSpace("accountId", AccountId);
                res.AddStringIfNotEmptyOrWhiteSpace("currency", Currency);

                return res;
            }
        }
    }
}
