using System.Runtime.Serialization;

namespace TinkoffMapper.Invest.Rest.Common.Data.Enum
{
    /// <summary>
    /// Тип заявки.
    /// </summary>
    public enum OrderTypeEnum
    {
        [EnumMember(Value = @"ORDER_TYPE_UNSPECIFIED")]
        ORDER_TYPE_UNSPECIFIED = 0,

        [EnumMember(Value = @"ORDER_TYPE_LIMIT")]
        ORDER_TYPE_LIMIT = 1,

        [EnumMember(Value = @"ORDER_TYPE_MARKET")]
        ORDER_TYPE_MARKET = 2,
    }
}
