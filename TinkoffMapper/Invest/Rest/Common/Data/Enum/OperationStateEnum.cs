using System.Runtime.Serialization;

namespace TinkoffMapper.Invest.Rest.Common.Data.Enum
{
    /// <summary>
    /// Статус запрашиваемых операций
    /// </summary>
    public enum OperationStateEnum
    {
        /// <summary>
        /// Статус операции не определён
        /// </summary>
        [EnumMember(Value = @"OPERATION_STATE_UNSPECIFIED")]
        OPERATION_STATE_UNSPECIFIED = 0,

        /// <summary>
        /// Исполнена
        /// </summary>
        [EnumMember(Value = @"OPERATION_STATE_EXECUTED")]
        OPERATION_STATE_EXECUTED = 1,

        /// <summary>
        /// Отменена
        /// </summary>
        [EnumMember(Value = @"OPERATION_STATE_CANCELED")]
        OPERATION_STATE_CANCELED = 2,

        /// <summary>
        /// Исполняется
        /// </summary>
        [EnumMember(Value = @"OPERATION_STATE_PROGRESS")]
        OPERATION_STATE_PROGRESS = 3,
    }
}
