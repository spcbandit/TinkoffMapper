using Newtonsoft.Json;
using System.Collections.Generic;
using Tinkoff.Proto.InvestApi.V1;
using TinkoffMapper.Extensions;
using TinkoffMapper.Requests;
using TinkoffMapper.Invest.Rest.Data.Enum;
using TinkoffMapper.Invest.Rest.Data.Market;

namespace TinkoffMapper.Invest.Rest.Request.Account
{
    /// <summary>
    /// Запрос выставления торгового поручения.
    /// </summary>
    public sealed class PostOrderRequest : RequestInvest
    {
        [JsonConstructor]
        public PostOrderRequest(string figi, string instrument_id, int quantity, QuotationData price, OrderDirectionEnum direction,
            string accountId, OrderTypeEnum orderType, string orderId)
        {
            Figi = figi;
            InstrumentId = instrument_id;
            Quantity = quantity;
            Price = price;
            Direction = direction;
            AccountId = accountId;
            OrderType = orderType;
            OrderId = orderId;
        }
        [JsonProperty("figi")]
        public string Figi { get; set; }
        [JsonProperty("instrumentId")]
        public string InstrumentId { get; set; }
        [JsonProperty("quantity")]
        public int? Quantity { get; set; }
        [JsonProperty("price")]
        public QuotationData Price { get; set; }
        [JsonProperty("direction")]
        public OrderDirectionEnum Direction { get; set; }
        [JsonProperty("accountId")]
        public string AccountId { get; set; }
        [JsonProperty("orderType")]
        public OrderTypeEnum OrderType { get; set; }
        [JsonProperty("orderId")]
        public string OrderId { get; set; }

        internal override string EndPoint => "/tinkoff.public.invest.api.contract.v1.OrdersService/PostOrder";
        internal override string SandboxEndPoint => "/tinkoff.public.invest.api.contract.v1.SandboxService/PostSandboxOrder";

        internal override RequestMethod Method => RequestMethod.POST;

        internal override object Body
        {
            get
            {
                var res = new Dictionary<string, object>();

                res.Add("orderType", OrderType);
                res.Add("direction", Direction);
                res.Add("figi", Figi);
                res.Add("quantity", Quantity);
                res.Add("accountId", AccountId);
                res.Add("instrumentId", InstrumentId);

                var price = new Dictionary<string, object>();

                price.Add("nano", Price.Nano);
                price.Add("units", Price.Units);

                res.AddObjectIfNotNull("price", price);

                return res;
            }
        }
    }
}
