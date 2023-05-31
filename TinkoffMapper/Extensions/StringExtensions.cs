using System.Globalization;

namespace TinkoffMapper.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Parse from invariant culture
        /// </summary>
        public static double DoubleParseIv(this string value)
        {
            return double.Parse(value, CultureInfo.InvariantCulture);
        }
    }
}
