using System.Collections.Generic;
using TinkoffMapper.Extensions;
using TinkoffMapper.Requests;

namespace TinkoffMapper.Invest.Rest.Request.Market
{
    /// <summary>
    /// Запрос стакана
    /// </summary>
    public sealed class GetOrderBookRequest : RequestInvest
    {
        public GetOrderBookRequest(string instrument_id)
        {
            InstrumentId = instrument_id;
        }
        public int? Depth { get; set; } = 50;
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
