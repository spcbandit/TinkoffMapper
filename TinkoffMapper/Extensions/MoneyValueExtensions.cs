using System;
using Tinkoff.Proto.InvestApi.V1;
using TinkoffMapper.Invest.Rest.Common.Data.Market;

namespace TinkoffMapper.Extensions
{
    public static class MoneyValueExtensions
    {
        /// <summary>
        /// Приводит тип QuotationData к double
        /// </summary>
        public static double ToDouble(this MoneyValue moneyValue)
        {
            try
            {
                return (moneyValue.Units + moneyValue.Nano / Math.Pow(10, 9)).Normalize();
            }
            catch (Exception e)
            {
            }
            return 0;
        }

        /// <summary>
        /// Приводит тип QuotationData к double
        /// </summary>
        public static double ToDouble(this MoneyValueData moneyValueData)
        {
            try
            {
                long units = long.Parse(moneyValueData.Units);
                return units + moneyValueData.Nano / Math.Pow(10, 9).Normalize();
            }
            catch (Exception e)
            {
            }
            return 0;
        }
    }
}

