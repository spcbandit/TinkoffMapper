using System.Collections.Generic;
using TinkoffMapper.Extensions;
using TinkoffMapper.Invest.Rest.Common.Data.Enum;
using TinkoffMapper.Requests;

namespace TinkoffMapper.Invest.Rest.Common.Request.Account
{
    public class CustomQuotation
    {
        public long units { get; set; }
        public long nano { get; set; }
    }
    
    public sealed class PostOrderRequest : RequestInvest
    {
        //https://tinkoff.github.io/investAPI/orders/#postorderrequest
        /// <summary>
        /// Запрос выставления торгового поручения
        /// </summary>
        /// <param name="figi">Deprecated Figi-идентификатор инструмента. Необходимо использовать instrument_id.</param>
        /// <param name="instrument_id">Идентификатор инструмента, принимает значения Figi или Instrument_uid.</param>
        /// <param name="quantity">Количество лотов</param>
        /// <param name="price">Цена за 1 инструмент. Для получения стоимости лота требуется умножить на лотность инструмента. Игнорируется для рыночных поручений</param>
        /// <param name="direction">Направление операции</param>
        /// <param name="accountId">Номер счёта</param>
        /// <param name="orderType">Тип заявки</param>
        /// <param name="orderId">Идентификатор запроса выставления поручения для целей идемпотентности. Максимальная длина 36 символов</param>
        public PostOrderRequest(string figi, string instrument_id, int quantity, CustomQuotation price, OrderDirectionEnum direction,
            string accountId, OrderTypeEnum orderType, string orderId)
        {
            Figi = figi;
            InstrumentId = instrument_id;
            Quantity = quantity;
            Price = price;
            Direction = direction;
            AccountId = accountId;
            OrderType = orderType;
            OrderId = orderId;
        }

        public string Figi { get; set; }
        public string InstrumentId { get; set; }
        public int? Quantity { get; set; }
        public CustomQuotation Price { get; set; }
        public OrderDirectionEnum Direction { get; set; }
        public string AccountId { get; set; }
        public OrderTypeEnum OrderType { get; set; }
        public string OrderId { get; set; }

        internal override string EndPoint => "/tinkoff.public.invest.api.contract.v1.OrdersService/PostOrder";
        internal override string SandboxEndPoint => "/tinkoff.public.invest.api.contract.v1.SandboxService/PostSandboxOrder";

        internal override RequestMethod Method => RequestMethod.POST;

        internal override object Body
        {
            get
            {
                var res = new Dictionary<string, object>();

                res.Add("orderType", OrderType);
                res.Add("direction", Direction);
                res.Add("figi", Figi);
                res.Add("quantity", Quantity);
                res.Add("accountId", AccountId);
                res.Add("instrumentId", InstrumentId);
                res.AddObjectIfNotNull("price", Price);

                return res;
            }
        }
    }
}
