using System.Collections.Generic;
using TinkoffMapper.Extensions;
using TinkoffMapper.Requests;
using TinkoffMapper.Invest.Rest.Data.Enum;

namespace TinkoffMapper.Invest.Rest.Request.Market
{
    /// <summary>
    /// Запрос получения инструментов.
    /// </summary>
    public sealed class InstrumentRequest : RequestInvest
    {
        public InstrumentRequest(InstrumentStatusEnum instrumentStatus, InstrumentTypeEnum instrumentType = InstrumentTypeEnum.Futures)
        {
            InstrumentStatus = instrumentStatus;
            InstrumentType = instrumentType;
        }
        public InstrumentStatusEnum InstrumentStatus { get; set; }
        public InstrumentTypeEnum InstrumentType { get; set; }

        internal override string EndPoint => $"/tinkoff.public.invest.api.contract.v1.InstrumentsService/{InstrumentType.GetEnumMemberAttributeValue()}";
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
