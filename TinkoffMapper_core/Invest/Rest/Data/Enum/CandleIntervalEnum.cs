using System.Runtime.Serialization;

namespace TinkoffMapper.Invest.Rest.Data.Enum
{
    /// <summary>
    /// Интервал запрошенных свечей.
    /// </summary>
    public enum CandleIntervalEnum
    {
        [EnumMember(Value = @"CANDLE_INTERVAL_UNSPECIFIED")]
        CANDLE_INTERVAL_UNSPECIFIED = 0,

        [EnumMember(Value = @"CANDLE_INTERVAL_1_MIN")]
        CANDLE_INTERVAL_1_MIN = 1,

        [EnumMember(Value = @"CANDLE_INTERVAL_5_MIN")]
        CANDLE_INTERVAL_5_MIN = 2,

        [EnumMember(Value = @"CANDLE_INTERVAL_15_MIN")]
        CANDLE_INTERVAL_15_MIN = 3,

        [EnumMember(Value = @"CANDLE_INTERVAL_HOUR")]
        CANDLE_INTERVAL_HOUR = 4,

        [EnumMember(Value = @"CANDLE_INTERVAL_DAY")]
        CANDLE_INTERVAL_DAY = 5,
    }
}
