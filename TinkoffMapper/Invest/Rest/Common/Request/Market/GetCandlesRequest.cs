using System;
using System.Collections.Generic;
using TinkoffMapper.Extensions;
using TinkoffMapper.Invest.Rest.Common.Data.Enum;
using TinkoffMapper.Requests;

namespace TinkoffMapper.Invest.Rest.Common.Request.Market
{
    public sealed class GetCandlesRequest : RequestInvest
    {
        //https://tinkoff.github.io/investAPI/marketdata/#getcandles
        /// <summary>
        /// Запрос исторических свечей
        /// </summary>
        /// <param name="startTime">Начало запрашиваемого периода в часовом поясе UTC</param>
        /// <param name="endTime">Окончание запрашиваемого периода в часовом поясе UTC</param>
        /// <param name="interval">Интервал запрошенных свечей</param>
        /// <param name="figi">Deprecated Figi-идентификатор инструмента. Необходимо использовать instrument_id</param>
        public GetCandlesRequest(DateTime startTime, DateTime endTime, CandleIntervalEnum interval, string figi)
        {
            StartTime = startTime;
            EndTime = endTime;
            Interval = interval;
            Figi = figi;
        }

        public string Figi { get; set; }


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
        internal override object Body
        {
            get
            {
                var res = new Dictionary<string, string>();

                res.AddStringIfNotEmptyOrWhiteSpace("from", StartTimeInMs);
                res.AddStringIfNotEmptyOrWhiteSpace("to", EndTimeInMs);
                res.AddEnum("interval", Interval);
                res.AddStringIfNotEmptyOrWhiteSpace("figi", Figi);
                return res;
            }
        }
    }
}
