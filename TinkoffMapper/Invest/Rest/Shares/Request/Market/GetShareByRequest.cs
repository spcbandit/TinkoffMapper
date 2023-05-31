using System;
using System.Collections.Generic;
using TinkoffMapper.Extensions;
using TinkoffMapper.Invest.Rest.Common.Data.Enum;
using TinkoffMapper.Requests;

namespace TinkoffMapper.Invest.Rest.Shares.Request.Market
{
    //https://tinkoff.github.io/investAPI/instruments/#shares
    /// <summary>
    /// Запрос получения акции по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор запрашиваемого инструмента</param>
    /// <param name="class_code">Идентификатор class_code. Обязателен при id_type = ticker</param>
    /// <param name="id_type">Тип идентификатора инструмента</param>
    /// <exception cref="ArgumentException"></exception>
    public class GetShareByRequest : RequestInvest
    {
        public GetShareByRequest(string id, string class_code = "", InstrumentIdTypeEnum id_type = InstrumentIdTypeEnum.INSTRUMENT_ID_TYPE_FIGI)
        {
            if (id_type == InstrumentIdTypeEnum.INSTRUMENT_ID_TYPE_TICKER && string.IsNullOrEmpty(class_code))
                throw new ArgumentException(nameof(class_code));

            ClassCode = class_code;
            IdType = id_type;
            Id = id;
        }

        public string Id { get; set; }
        public string ClassCode { get; set; }
        public InstrumentIdTypeEnum IdType { get; set; }

        internal override string EndPoint => "/tinkoff.public.invest.api.contract.v1.InstrumentsService/ShareBy";
        internal override RequestMethod Method => RequestMethod.POST;
        internal override object Body
        {
            get
            {
                var res = new Dictionary<string, string>();

                res.AddObjectIfNotNull("id", Id);
                res.Add("classCode", ClassCode);
                res.AddObjectIfNotNull("idType", IdType.GetEnumMemberAttributeValue());

                return res;
            }
        }
    }
}
