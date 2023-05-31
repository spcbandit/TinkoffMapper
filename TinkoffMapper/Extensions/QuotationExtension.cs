using System;
using Tinkoff.Proto.InvestApi.V1;
using TinkoffMapper.Invest.Rest.Common.Request.Account;
using TinkoffMapper.Invest.Rest.Common.Data.Market;

namespace TinkoffMapper.Extensions
{
    public static class QuotationHelper
    {
        /// <summary>
        /// Приводит тип Quotation к double
        /// </summary>
        public static double ToDouble(this Quotation quotation)
        {
            try
            {
                return (quotation.Units + quotation.Nano / Math.Pow(10, 9)).Normalize();
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        /// <summary>
        /// Приводит тип QuotationData к double
        /// </summary>
        public static double ToDouble(this QuotationData quotationData)
        {
            try
            {
                int units = 0;
                int nano = 0;

                if(quotationData.Units.HasValue)
                    units = quotationData.Units.Value;

                if(quotationData.Nano.HasValue)
                    nano = quotationData.Nano.Value;

                return units + nano / Math.Pow(10, 9).Normalize();
            }
            catch (Exception e)
            {
            }
            return 0;
        }

        /// <summary>
        /// Приводит тип decimal к Quotation  
        /// </summary>
        public static Quotation ToQuotation(this decimal price)
        {
            var quotation = new Quotation();

            try
            {
                quotation.Units = (long)price;
                quotation.Nano = (int)((price - quotation.Units) * (decimal)Math.Pow(10, 9));
            }
            catch (Exception e)
            {

            }

            return quotation;
        }
        /// <summary>
        /// Приводит тип decimal к Quotation  
        /// </summary>
        public static CustomQuotation ToCustomQuotation(this decimal price)
        {
            var quotation = new CustomQuotation();

            try
            {
                quotation.units = (long)price;
                quotation.nano = (int)((price - quotation.units) * (decimal)Math.Pow(10, 9));
            }
            catch (Exception e)
            {

            }

            return quotation;
        }
    }
}
