using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TinkoffMapper.Extensions;
using TinkoffMapper.Invest.Rest.Common.Data.Enum;
using TinkoffMapper.Requests;

namespace TinkoffMapper.Invest.Rest.Common.Request.Account
{
    public sealed class GetOperationsRequest : RequestInvest
    {
        //https://tinkoff.github.io/investAPI/operations/#getoperations
        /// <summary>
        /// Запрос получения списка операций по счёту.
        /// </summary>
        /// <param name="accountId">Идентификатор счёта клиента</param>
        /// <param name="state">Начало периода (по UTC)</param>
        /// <param name="figi">Окончание периода (по UTC)</param>
        /// <param name="endTime">Статус запрашиваемых операций</param>
        /// <param name="startTime">Figi-идентификатор инструмента для фильтрации</param>
        public GetOperationsRequest(string accountId, OperationStateEnum state, string figi, DateTime? endTime, DateTime? startTime)
        {
            AccountId = accountId;
            StartTime = startTime;
            EndTime = endTime;
            State = state;
            Figi = figi;
        }
        
        public string AccountId { get; set; }

        /// <summary>
        /// Сериализация времени для реквеста
        /// </summary>
        public DateTime? StartTime
        {
            get => Convert.ToDateTime(StartTimeInMs);
            set
            {
                if (value.HasValue)
                    StartTimeInMs = value.Value.ToString("yyyy-MM-ddTH:mm:ss.fffffffZ");
            }
        }
        public DateTime? EndTime
        {
            get { return Convert.ToDateTime(EndTimeInMs); }
            set
            {
                if (value.HasValue)
                    EndTimeInMs = value.Value.ToString("yyyy-MM-ddTH:mm:ss.fffffffZ");
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

                if(!string.IsNullOrEmpty(StartTimeInMs))
                    res.AddStringIfNotEmptyOrWhiteSpace("from", StartTimeInMs);
                if (!string.IsNullOrEmpty(EndTimeInMs))
                    res.AddStringIfNotEmptyOrWhiteSpace("to", EndTimeInMs);

                res.AddEnum("state", State);
                res.AddStringIfNotEmptyOrWhiteSpace("figi", Figi);

                return res;
            }
        }
    }
}
