using System.Collections.Generic;

using TinkoffMapper.Extensions;
using TinkoffMapper.Requests;

namespace TinkoffMapper.Invest.Rest.Request.Account
{
    /// <summary>
    /// Запрос доступного для вывода остатка.
    /// </summary>
    public sealed class WithdrawLimitsRequest : RequestInvest
    {
        public WithdrawLimitsRequest(string accountId)
        {
            AccountId = accountId;
        }

        public string AccountId { get; set; }
        internal override string EndPoint => "/tinkoff.public.invest.api.contract.v1.OperationsService/GetWithdrawLimits";
        internal override string SandboxEndPoint => "/tinkoff.public.invest.api.contract.v1.SandboxService/GetSandboxWithdrawLimits";

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
