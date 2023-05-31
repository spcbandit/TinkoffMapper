using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TinkoffMapper.Extensions;
using TinkoffMapper.Invest.Rest.Common.Data.Enum;
using TinkoffMapper.Invest.Rest.Common.Data.Market;

namespace TinkoffMapper.Invest.Rest.Common.Data.Account
{
    /// <summary>
    /// Данные по операции.
    /// </summary>
    public sealed class OperationData
    {
        [JsonConstructor]
        public OperationData(string id, string parentOperationId, string currency, MoneyValueData payment, MoneyValueData price, 
            string state, string quantity, string quantityRest, string figi, string instrumentType, DateTimeOffset date, 
            string type, string operationType, List<OperationTrade> trades)
        {
            Id = id;
            ParentOperationId = parentOperationId;
            Currency = currency;
            Payment = payment;
            Price = price;
            State = state;
            StateEnum = State.ToEnum<OperationStateEnum>();
            Quantity = quantity;
            QuantityRest = quantityRest;
            Figi = figi;
            InstrumentType = instrumentType;
            Date = date;
            Type = type;
            OperationType = operationType;
            OperationTypeEnum = OperationType.ToEnum<OperationTypeEnum>();
            Trades = trades;
        }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("parentOperationId")]
        public string ParentOperationId { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("payment")]
        public MoneyValueData Payment { get; set; }

        [JsonProperty("price")]
        public MoneyValueData Price { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }
        public OperationStateEnum StateEnum { get; set; }

        [JsonProperty("quantity")]
        public string Quantity { get; set; }

        [JsonProperty("quantityRest")]
        public string QuantityRest { get; set; }

        [JsonProperty("figi")]
        public string Figi { get; set; }

        [JsonProperty("instrumentType")]
        public string InstrumentType { get; set; }

        [JsonProperty("date")]
        public DateTimeOffset Date { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("operationType")]
        public string OperationType { get; set; }
        public OperationTypeEnum OperationTypeEnum { get; set; }
        
        [JsonProperty("trades")]
        public List<OperationTrade> Trades { get; set; }
    }
}
