using Newtonsoft.Json;
using System.Collections.Generic;
using Tinkoff.Proto.InvestApi.V1;
using TinkoffMapper.Requests;
using TinkoffTester.Data;
using TinkoffTester.Extensions;
using TinkoffTester.Request;

namespace TinkoffTester.Sandbox.Request
{
    internal class SandboxPayInRequest : SandboxRequestInvest
    {
        public SandboxPayInRequest(string accountId, Amount amount)
        {
            AccountId = accountId;
            Amount = amount;
        }
        public string AccountId { get; set; }
        public Amount Amount { get; set; }
        internal override string EndPoint => "/tinkoff.public.invest.api.contract.v1.SandboxService/SandboxPayIn";
        internal override RequestMethod Method => RequestMethod.POST;
        internal override object Body
        {
            get
            {
                var res = new Dictionary<string, string>();

                res.AddStringIfNotEmptyOrWhiteSpace("accountId", AccountId);
                res.AddStringIfNotEmptyOrWhiteSpace("amount", JsonConvert.SerializeObject(Amount));


                var accountId = AccountId;
                var amount = Amount;

                return new
                {
                    accountId,
                    amount,
                };
            }
        }
    }
}
