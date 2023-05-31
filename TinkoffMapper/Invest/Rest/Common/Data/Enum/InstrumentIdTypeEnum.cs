using System.Runtime.Serialization;

namespace TinkoffMapper.Invest.Rest.Common.Data.Enum
{
    /// <summary>
    /// Идентификация торговых инструментов
    /// </summary>
    public enum InstrumentIdTypeEnum
    {
        /// <summary>
        /// Значение не определено
        /// </summary>
        [EnumMember(Value = @"INSTRUMENT_ID_UNSPECIFIED")]
        INSTRUMENT_ID_UNSPECIFIED,

        /// <summary>
        /// Figi
        /// </summary>
        [EnumMember(Value = @"INSTRUMENT_ID_TYPE_FIGI")]
        INSTRUMENT_ID_TYPE_FIGI,
        
        /// <summary>
        /// Ticker
        /// </summary>
        [EnumMember(Value = @"INSTRUMENT_ID_TYPE_TICKER")]
        INSTRUMENT_ID_TYPE_TICKER,
        
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        [EnumMember(Value = @"INSTRUMENT_ID_TYPE_UID")]
        INSTRUMENT_ID_TYPE_UID,

        /// <summary>
        /// Идентификатор позиции
        /// </summary>
        [EnumMember(Value = @"INSTRUMENT_ID_TYPE_POSITION_UID")]
        INSTRUMENT_ID_TYPE_POSITION_UID,
    }
}
