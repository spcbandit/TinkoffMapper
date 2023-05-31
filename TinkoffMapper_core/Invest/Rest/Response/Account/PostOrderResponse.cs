using Newtonsoft.Json;
using TinkoffMapper.Extensions;
using TinkoffMapper.Invest.Rest.Data.Enum;
using TinkoffMapper.Invest.Rest.Data.Market;

namespace TinkoffMapper.Invest.Rest.Response.Account
{
    /// <summary>
    /// Информация о выставлении поручения.
    /// </summary>
    public sealed class PostOrderResponse
    {
        [JsonConstructor]
        public PostOrderResponse(string orderId, string executionReportStatus,string lotsRequested, string lotsExecuted, 
            MoneyValueData initialOrderPrice, MoneyValueData executedOrderPrice, MoneyValueData totalOrderAmount, 
            MoneyValueData initialCommission, MoneyValueData executedCommission, MoneyValueData aciValue, string figi, 
            string direction, MoneyValueData initialSecurityPrice, string orderType, OrderTypeEnum orderTypeEnum, string message, 
            QuotationData initialOrderPricePt)
        {
            OrderId = orderId;
            ExecutionReportStatus = executionReportStatus;
            ExecutionReportStatusEnum = ExecutionReportStatus.ToEnum<OrderExecutionReportStatusEnum>();
            LotsRequested = lotsRequested;
            LotsExecuted = lotsExecuted;
            InitialOrderPrice = initialOrderPrice;
            ExecutedOrderPrice = executedOrderPrice;
            TotalOrderAmount = totalOrderAmount;
            InitialCommission = initialCommission;
            ExecutedCommission = executedCommission;
            AciValue = aciValue;
            Figi = figi;
            Direction = direction;
            DirectionEnum = Direction.ToEnum<OrderDirectionEnum>();
            InitialSecurityPrice = initialSecurityPrice;
            OrderType = orderType;
            OrderTypeEnum = orderTypeEnum;
            Message = message;
            InitialOrderPricePt = initialOrderPricePt;
        }

        [JsonProperty("orderId")]
        public string OrderId { get; set; }

        [JsonProperty("executionReportStatus")]
        public string ExecutionReportStatus { get; set; }
        public OrderExecutionReportStatusEnum ExecutionReportStatusEnum { get; set; }
        [JsonProperty("lotsRequested")]
        public string LotsRequested { get; set; }

        [JsonProperty("lotsExecuted")]
        public string LotsExecuted { get; set; }

        [JsonProperty("initialOrderPrice")]
        public MoneyValueData InitialOrderPrice { get; set; }

        [JsonProperty("executedOrderPrice")]
        public MoneyValueData ExecutedOrderPrice { get; set; }

        [JsonProperty("totalOrderAmount")]
        public MoneyValueData TotalOrderAmount { get; set; }

        [JsonProperty("initialCommission")]
        public MoneyValueData InitialCommission { get; set; }

        [JsonProperty("executedCommission")]
        public MoneyValueData ExecutedCommission { get; set; }

        [JsonProperty("aciValue")]
        public MoneyValueData AciValue { get; set; }

        [JsonProperty("figi")]
        public string Figi { get; set; }

        [JsonProperty("direction")]
        public string Direction { get; set; }
        public OrderDirectionEnum DirectionEnum { get; set; }
        [JsonProperty("initialSecurityPrice")]
        public MoneyValueData InitialSecurityPrice { get; set; }

        [JsonProperty("orderType")]
        public string OrderType { get; set; }
        public OrderTypeEnum OrderTypeEnum { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("initialOrderPricePt")]
        public QuotationData InitialOrderPricePt { get; set; }
    }
}
