using System.Runtime.Serialization;

namespace TinkoffMapper.Invest.Rest.Common.Data.Enum
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

        [EnumMember(Value = @"CANDLE_INTERVAL_DAY")]
        CANDLE_INTERVAL_2_MIN = 6,

        [EnumMember(Value = @"CANDLE_INTERVAL_3_MIN")]
        CANDLE_INTERVAL_3_MIN = 7,
        [EnumMember(Value = @"CANDLE_INTERVAL_10_MIN")]
        CANDLE_INTERVAL_10_MIN = 8,
        [EnumMember(Value = @"CANDLE_INTERVAL_30_MIN")]
        CANDLE_INTERVAL_30_MIN = 9,
        [EnumMember(Value = @"CANDLE_INTERVAL_2_HOUR")]
        CANDLE_INTERVAL_2_HOUR = 10,
        [EnumMember(Value = @"CANDLE_INTERVAL_4_HOUR")]
        CANDLE_INTERVAL_4_HOUR = 11,
        [EnumMember(Value = @"CANDLE_INTERVAL_WEEK")]
        CANDLE_INTERVAL_WEEK = 12,
        [EnumMember(Value = @"CANDLE_INTERVAL_MONTH")]
        CANDLE_INTERVAL_MONTH = 13,

    }
}
