using System.Collections.Generic;
using TinkoffMapper.Extensions;
using TinkoffMapper.Requests;

namespace TinkoffMapper.Invest.Rest.Common.Request.Account
{
    public sealed class CancelOrderRequest : RequestInvest
    {
        //https://tinkoff.github.io/investAPI/orders/#cancelorder
        /// <summary>
        /// Запрос отмены торгового поручения
        /// </summary>
        /// <param name="accountId">Номер счёта</param>
        /// <param name="orderId">Идентификатор заявки</param>
        public CancelOrderRequest(string accountId, string orderId)
        {
            AccountId = accountId;
            OrderId = orderId;
        }
        public string AccountId { get; set; }
        public string OrderId { get; set; }

        internal override string EndPoint => "/tinkoff.public.invest.api.contract.v1.OrdersService/CancelOrder";
        internal override string SandboxEndPoint => "/tinkoff.public.invest.api.contract.v1.SandboxService/CancelSandboxOrder";

        internal override RequestMethod Method => RequestMethod.POST;
        internal override object Body
        {
            get
            {
                var res = new Dictionary<string, string>();

                res.AddStringIfNotEmptyOrWhiteSpace("accountId", AccountId);
                res.AddStringIfNotEmptyOrWhiteSpace("orderId", OrderId);

                return res;
            }
        }
    }
}
