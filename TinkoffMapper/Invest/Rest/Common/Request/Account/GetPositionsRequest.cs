using System.Collections.Generic;
using TinkoffMapper.Extensions;
using TinkoffMapper.Requests;

namespace TinkoffMapper.Invest.Rest.Common.Request.Account
{
    public sealed class GetPositionsRequest : RequestInvest
    {
        //https://tinkoff.github.io/investAPI/operations/#getpositions
        /// <summary>
        /// Запрос позиций портфеля по счёту
        /// </summary>
        /// <param name="accountId">Идентификатор счёта пользователя</param>
        public GetPositionsRequest(string accountId)
        {
            AccountId = accountId;
        }

        public string AccountId { get; set; }
        internal override string EndPoint => "/tinkoff.public.invest.api.contract.v1.OperationsService/GetPositions";
        internal override string SandboxEndPoint => "/tinkoff.public.invest.api.contract.v1.SandboxService/GetSandboxPositions";

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
