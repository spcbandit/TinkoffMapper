using System;
using System.Collections.Generic;

using TinkoffMapper.Extensions;
using TinkoffMapper.Requests;
using TinkoffMapper.Invest.Rest.Data.Enum;

namespace TinkoffMapper.Invest.Rest.Request.Market
{
    /// <summary>
    /// Запрос исторических свечей.
    /// </summary>
    public sealed class GetCandlesRequest : RequestInvest
    {
        public GetCandlesRequest(DateTime startTime, DateTime endTime, CandleIntervalEnum interval, string instrument_id)
        {
            StartTime = startTime;
            EndTime = endTime;
            Interval = interval;
            InstrumentId = instrument_id;
        }
        
        public string InstrumentId { get; set; }


        /// <summary>
        /// Сериализация времени для реквеста
        /// </summary>
        public DateTime StartTime
        {
            get => Convert.ToDateTime(StartTimeInMs);
            set
            {
                if (value != null)
                    StartTimeInMs = value.ToString("yyyy-MM-ddTH:mm:ss.fffffffZ");
            }
        }
        public DateTime EndTime
        {
            get { return Convert.ToDateTime(EndTimeInMs); }
            set
            {
                if (value != null)
                    EndTimeInMs = value.ToString("yyyy-MM-ddTH:mm:ss.fffffffZ");
            }
        }

        private string StartTimeInMs { get; set; }
        private string EndTimeInMs { get; set; }
        public CandleIntervalEnum Interval { get; set; }
        internal override string EndPoint => "/tinkoff.public.invest.api.contract.v1.MarketDataService/GetCandles";
        internal override RequestMethod Method => RequestMethod.POST;
        internal override object Body {
            get
            {
                var res = new Dictionary<string, string>();
                
                res.AddStringIfNotEmptyOrWhiteSpace("from", StartTimeInMs);
                res.AddStringIfNotEmptyOrWhiteSpace("to", EndTimeInMs);
                res.AddEnum("interval", Interval);
                res.AddStringIfNotEmptyOrWhiteSpace("instrument_id", InstrumentId);
                return res;
            }
        }
    }
}
