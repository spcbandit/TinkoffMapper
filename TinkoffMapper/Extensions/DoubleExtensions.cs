using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinkoffMapper.Extensions
{
    public static class DoubleExtensions
    {
        public static double Normalize(this double value, int decimals = 10)
        {
            return Math.Round(value, decimals, MidpointRounding.AwayFromZero);
        }
    }
}
