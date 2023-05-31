using System.Runtime.Serialization;

namespace TinkoffMapper.Invest.Rest.Data.Enum
{
    /// <summary>
    /// Статус запрашиваемых операций
    /// </summary>
    public enum OperationStateEnum
    {
        [EnumMember(Value = @"OPERATION_STATE_UNSPECIFIED")]
        OPERATION_STATE_UNSPECIFIED = 0,

        [EnumMember(Value = @"OPERATION_STATE_EXECUTED")]
        OPERATION_STATE_EXECUTED = 1,

        [EnumMember(Value = @"OPERATION_STATE_CANCELED")]
        OPERATION_STATE_CANCELED = 2,
    }
}
