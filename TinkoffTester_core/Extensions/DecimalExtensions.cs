using JetBrains.Annotations;

using System.Globalization;

namespace TinkoffMapper.Extensions
{
    /// <summary>
    /// конвертация в стринг
    /// </summary>
    internal static class DecimalExtensions
    {
        private static readonly NumberFormatInfo NumberFormat = new NumberFormatInfo { NumberDecimalSeparator = @"." };

        [NotNull]
        public static string ToFormattedString(this decimal value) => value.ToString(NumberFormat);
    }
}
