using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinkoffMapper.Requests;
using TinkoffTester.Data;
using TinkoffTester.Extensions;
using TinkoffTester.Request;

namespace TinkoffTester.Sandbox.Request
{
    internal class CloseSandboxAccountRequest : SandboxRequestInvest
    {
        public CloseSandboxAccountRequest(string accountId)
        {
            AccountId = accountId;
        }
        public string AccountId { get; set; }
        internal override string EndPoint => "/tinkoff.public.invest.api.contract.v1.SandboxService/CloseSandboxAccount";
        internal override RequestMethod Method => RequestMethod.POST;
        internal override object Body
        {
            get
            {
                var res = new Dictionary<string, string>();

                res.AddStringIfNotEmptyOrWhiteSpace("accountId", AccountId);

                return res;
            }
        }
    }
}
