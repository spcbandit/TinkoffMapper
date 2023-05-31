using System.Runtime.Serialization;

namespace TinkoffMapper.Invest.Rest.Common.Data.Enum
{
    /// <summary>
    /// Статус запрашиваемых инструментов.
    /// </summary>
    public enum InstrumentStatusEnum
    {
        /// <summary>
        /// Значение не определено
        /// </summary>
        [EnumMember(Value = @"INSTRUMENT_STATUS_UNSPECIFIED")]
        INSTRUMENT_STATUS_UNSPECIFIED = 0,

        /// <summary>
        /// Базовый список инструментов (по умолчанию). Инструменты доступные для торговли через TINKOFF INVEST API
        /// </summary>
        [EnumMember(Value = @"INSTRUMENT_STATUS_BASE")]
        INSTRUMENT_STATUS_BASE = 1,

        /// <summary>
        /// Список всех инструментов
        /// </summary>
        [EnumMember(Value = @"INSTRUMENT_STATUS_ALL")]
        INSTRUMENT_STATUS_ALL = 2,
    }
}
