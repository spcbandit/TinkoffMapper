using System.Collections.Generic;
using TinkoffMapper.Extensions;
using TinkoffMapper.Requests;
using TinkoffMapper.Invest.Rest.Common.Data.Enum;

namespace TinkoffMapper.Invest.Rest.Futures.Request.Market
{
    public sealed class FuturesRequest : RequestInvest
    {
        //https://tinkoff.github.io/investAPI/instruments/#futures
        /// <summary>
        /// Запрос списка фьючерсов
        /// </summary>
        /// <param name="instrumentStatus">Статус запрашиваемых инструментов</param>
        public FuturesRequest(InstrumentStatusEnum instrumentStatus)
        {
            InstrumentStatus = instrumentStatus;
        }
        public InstrumentStatusEnum InstrumentStatus { get; set; }

        internal override string EndPoint => $"/tinkoff.public.invest.api.contract.v1.InstrumentsService/Futures";
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
