using System.Runtime.Serialization;

namespace TinkoffMapper.Invest.Rest.Common.Data.Enum
{
    public enum OrderExecutionReportStatusEnum
    {
        [EnumMember(Value = @"EXECUTION_REPORT_STATUS_UNSPECIFIED")]
        EXECUTION_REPORT_STATUS_UNSPECIFIED = 0,

        [EnumMember(Value = @"EXECUTION_REPORT_STATUS_FILL")]
        EXECUTION_REPORT_STATUS_FILL = 1,

        [EnumMember(Value = @"EXECUTION_REPORT_STATUS_REJECTED")]
        EXECUTION_REPORT_STATUS_REJECTED = 2,

        [EnumMember(Value = @"EXECUTION_REPORT_STATUS_CANCELLED")]
        EXECUTION_REPORT_STATUS_CANCELLED = 3,

        [EnumMember(Value = @"EXECUTION_REPORT_STATUS_NEW")]
        EXECUTION_REPORT_STATUS_NEW = 4,

        [EnumMember(Value = @"EXECUTION_REPORT_STATUS_PARTIALLYFILL")]
        EXECUTION_REPORT_STATUS_PARTIALLYFILL = 5,
    }
}
