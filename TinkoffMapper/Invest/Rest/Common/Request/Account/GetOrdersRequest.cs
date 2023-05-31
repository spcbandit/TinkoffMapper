using System.Collections.Generic;
using TinkoffMapper.Extensions;
using TinkoffMapper.Requests;

namespace TinkoffMapper.Invest.Rest.Common.Request.Account
{
    public class GetOrdersRequest : RequestInvest
    {
        //https://tinkoff.github.io/investAPI/orders/#getorders
        /// <summary>
        /// Метод получения списка активных заявок по счёту
        /// </summary>
        /// <param name="accountId">Номер счёта</param>
        public GetOrdersRequest(string accountId)
        {
            AccountId = accountId;
        }

        public string AccountId { get; set; }
        internal override string EndPoint => "/tinkoff.public.invest.api.contract.v1.OrdersService/GetOrders";
        internal override string SandboxEndPoint => "";

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
