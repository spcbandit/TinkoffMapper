using System.Collections.Generic;
using TinkoffMapper.Extensions;
using TinkoffMapper.Requests;

namespace TinkoffMapper.Invest.Rest.Common.Request.Market
{
    public sealed class GetOrderBookRequest : RequestInvest
    {
        //https://tinkoff.github.io/investAPI/marketdata/#getorderbook
        /// <summary>
        /// Запрос стакана
        /// </summary>
        /// <param name="instrument_id">Идентификатор инструмента, принимает значение figi или instrument_uid</param>
        /// <param name="depth">Глубина стакана</param>
        public GetOrderBookRequest(string instrument_id, int depth = 50)
        {
            InstrumentId = instrument_id;
            Depth = depth;
        }
        public int? Depth { get; set; }
        public string InstrumentId { get; set; }

        internal override string EndPoint => "/tinkoff.public.invest.api.contract.v1.MarketDataService/GetOrderBook";
        internal override RequestMethod Method => RequestMethod.POST;
        internal override object Body
        {
            get
            {
                var res = new Dictionary<string, string>();
                res.AddSimpleStructIfNotNull("depth", Depth);
                res.AddStringIfNotEmptyOrWhiteSpace("instrument_id", InstrumentId);
                return res;
            }
        }
    }
}
