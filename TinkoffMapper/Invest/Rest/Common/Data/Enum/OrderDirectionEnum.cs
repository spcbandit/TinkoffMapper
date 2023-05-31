using System.Runtime.Serialization;

namespace TinkoffMapper.Invest.Rest.Common.Data.Enum
{
    /// <summary>
    /// Направление операции.
    /// </summary>
    public enum OrderDirectionEnum
    {
        [EnumMember(Value = @"ORDER_DIRECTION_UNSPECIFIED")]
        ORDER_DIRECTION_UNSPECIFIED = 0,

        [EnumMember(Value = @"ORDER_DIRECTION_BUY")]
        ORDER_DIRECTION_BUY = 1,

        [EnumMember(Value = @"ORDER_DIRECTION_SELL")]
        ORDER_DIRECTION_SELL = 2,
    }
}
