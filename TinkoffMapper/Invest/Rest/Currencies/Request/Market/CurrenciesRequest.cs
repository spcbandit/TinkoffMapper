﻿using System.Collections.Generic;
using TinkoffMapper.Extensions;
using TinkoffMapper.Invest.Rest.Common.Data.Enum;
using TinkoffMapper.Requests;

namespace TinkoffMapper.Invest.Rest.Currencies.Request.Market
{
    public class CurrenciesRequest : RequestInvest
    {
        //https://tinkoff.github.io/investAPI/instruments/#currencies
        /// <summary>
        /// Запрос списка валют
        /// </summary>
        /// <param name="instrumentStatus">Статус запрашиваемых инструментов</param>
        public CurrenciesRequest(InstrumentStatusEnum instrumentStatus)
        {
            InstrumentStatus = instrumentStatus;
        }
        public InstrumentStatusEnum InstrumentStatus { get; set; }

        internal override string EndPoint => $"/tinkoff.public.invest.api.contract.v1.InstrumentsService/Currencies";
        internal override RequestMethod Method => RequestMethod.POST;
        internal override object Body
        {
            get
            {
                var res = new Dictionary<string, string>();

                res.AddEnum("instrumentStatus", InstrumentStatus);

                return res;
            }
        }
    }
}
