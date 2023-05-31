using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;
using Tinkoff.Proto.InvestApi.V1;
using TinkoffMapper.Extensions;
using TinkoffMapper.Requests;

namespace TinkoffMapper.Invest.Rest.Request.Market
{
    /// <summary>
    /// Запрос получения цен последних сделок
    /// </summary>
    public sealed class GetLastPricesRequest : RequestInvest
    {
        public GetLastPricesRequest(string instrument_id)
        {
            InstrumentId = instrument_id;
        }
        
        public string InstrumentId { get; set; }

        internal override string EndPoint => "/tinkoff.public.invest.api.contract.v1.MarketDataService/GetLastPrices";
        internal override RequestMethod Method => RequestMethod.POST;
        internal override object Body
        {
            get
            {
                var res = new Dictionary<string, object>();
                var instruments = new[] { InstrumentId };
                
                res.AddObjectIfNotNull("instrumentId", instruments);

                return res;
            }
        }

    }
}
