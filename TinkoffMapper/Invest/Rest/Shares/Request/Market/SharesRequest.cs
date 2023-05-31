using System.Collections.Generic;
using TinkoffMapper.Extensions;
using TinkoffMapper.Requests;
using TinkoffMapper.Invest.Rest.Common.Data.Enum;

namespace TinkoffMapper.Invest.Rest.Shares.Request.Market
{
    public sealed class SharesRequest : RequestInvest
    {
        //https://tinkoff.github.io/investAPI/instruments/#shares
        /// <summary>
        /// Запрос списка акций
        /// </summary>
        /// <param name="instrumentStatus">Статус запрашиваемых инструментов</param>
        public SharesRequest(InstrumentStatusEnum instrumentStatus)
        {
            InstrumentStatus = instrumentStatus;
        }
        public InstrumentStatusEnum InstrumentStatus { get; set; }

        internal override string EndPoint => $"/tinkoff.public.invest.api.contract.v1.InstrumentsService/Shares";
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
