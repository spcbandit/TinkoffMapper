﻿using System;
using Newtonsoft.Json;
using TinkoffMapper.Invest.Rest.Common.Data.Market;

namespace TinkoffMapper.Invest.Rest.Common.Data.Market
{
    /// <summary>
    /// Информация о свече.
    /// </summary>
    public sealed class HistoricCandleData
    {
        [JsonConstructor]
        public HistoricCandleData(QuotationData open, QuotationData high, QuotationData low, QuotationData close, long volume,
            DateTimeOffset time, bool isComplete)
        {
            Open = open;
            High = high;
            Low = low;
            Close = close;
            Volume = volume;
            Time = time;
            IsComplete = isComplete;
        }

        [JsonProperty("open")]
        public QuotationData Open { get; set; }

        [JsonProperty("high")]
        public QuotationData High { get; set; }

        [JsonProperty("low")]
        public QuotationData Low { get; set; }

        [JsonProperty("close")]
        public QuotationData Close { get; set; }

        [JsonProperty("volume")]
        public long Volume { get; set; }

        [JsonProperty("time")]
        public DateTimeOffset Time { get; set; }

        [JsonProperty("isComplete")]
        public bool IsComplete { get; set; }
    }
}
