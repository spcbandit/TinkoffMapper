using System.Runtime.Serialization;

namespace TinkoffMapper.Invest.Rest.Data.Enum
{
    /// <summary>
    /// Статус запрашиваемых инструментов.
    /// </summary>
    public enum InstrumentStatusEnum
    {
        [EnumMember(Value = @"INSTRUMENT_STATUS_UNSPECIFIED")]
        INSTRUMENT_STATUS_UNSPECIFIED = 0,

        [EnumMember(Value = @"INSTRUMENT_STATUS_BASE")]
        INSTRUMENT_STATUS_BASE = 1,

        [EnumMember(Value = @"INSTRUMENT_STATUS_ALL")]
        INSTRUMENT_STATUS_ALL = 2,
    }
}
