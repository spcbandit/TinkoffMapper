using Google.Protobuf.WellKnownTypes;
using Newtonsoft.Json;
using Tinkoff.Proto.InvestApi.V1;

namespace TinkoffMapper.Invest.Rest.Common.Data.Account
{
    public class OperationTrade
    {
        [JsonProperty("trade_id")]
        public string TradeId { get; set; }
        
        [JsonProperty("date_time")]
        public Timestamp DateTime { get; set; }
        
        [JsonProperty("quantity")]
        public long Quantity { get; set; }
        
        [JsonProperty("price")]
        public MoneyValue Price { get; set; }
    }
}