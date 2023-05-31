using Newtonsoft.Json;

using System;
using System.Collections.Generic;

using TinkoffMapper.Extensions;
using TinkoffMapper.Requests;
using TinkoffMapper.Invest.Rest.Data.Enum;

namespace TinkoffMapper.Invest.Rest.Request.Account
{
    /// <summary>
    /// Запрос получения списка операций по счёту.
    /// </summary>
    public sealed class OperationsRequest : RequestInvest
    {
        public OperationsRequest(string accountId, DateTime endTime, DateTime startTime, OperationStateEnum state, string figi)
        {
            AccountId = accountId;
            StartTime = startTime;
            EndTime = endTime;
            State = state;
            Figi = figi;
        }

        [JsonProperty("accountId")]
        public string AccountId { get; set; }

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
        public OperationStateEnum State { get; set; }
        public string Figi { get; set; }
        internal override string EndPoint => "/tinkoff.public.invest.api.contract.v1.OperationsService/GetOperations";
        internal override string SandboxEndPoint => "/tinkoff.public.invest.api.contract.v1.SandboxService/GetSandboxOperations";

        internal override RequestMethod Method => RequestMethod.POST;
        internal override object Body
        {
            get
            {
                var res = new Dictionary<string, string>();

                res.AddStringIfNotEmptyOrWhiteSpace("accountId", AccountId);
                res.AddStringIfNotEmptyOrWhiteSpace("from", StartTimeInMs);
                res.AddStringIfNotEmptyOrWhiteSpace("to", EndTimeInMs);
                res.AddEnum("state", State);
                res.AddStringIfNotEmptyOrWhiteSpace("figi", Figi);

                return res;
            }
        }
    }
}
