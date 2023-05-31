using System.Collections.Generic;
using TinkoffMapper.Extensions;
using TinkoffMapper.Requests;

namespace TinkoffMapper.Invest.Rest.Common.Request.Market
{
    public sealed class GetLastPricesRequest : RequestInvest
    {
        //https://tinkoff.github.io/investAPI/marketdata/#getlastprices
        /// <summary>
        /// Запрос получения цен последних сделок
        /// </summary>
        /// <param name="instrument_id">Идентификатор инструмента</param>
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
