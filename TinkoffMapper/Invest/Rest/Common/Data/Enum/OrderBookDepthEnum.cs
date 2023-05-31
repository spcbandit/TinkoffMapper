using System.Runtime.Serialization;

namespace TinkoffMapper.Invest.Rest.Common.Data.Enum
{
    /// <summary>
    /// Глубина стакана
    /// </summary>
    public enum OrderBookDepthEnum
    {
        [EnumMember(Value = @"1")]
        ONE,
        [EnumMember(Value = @"10")]
        TEN,
        [EnumMember(Value = @"20")]
        TWENTY,
        [EnumMember(Value = @"30")]
        THIRTY,
        [EnumMember(Value = @"40")]
        FOURTY,
        [EnumMember(Value = @"50")]
        FIFTY
    }
}
