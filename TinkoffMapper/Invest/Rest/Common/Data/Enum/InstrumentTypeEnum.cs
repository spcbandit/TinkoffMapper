using System.Runtime.Serialization;

namespace TinkoffMapper.Invest.Rest.Common.Data.Enum
{
    /// <summary>
    /// Тип запрашиваемого инструмента
    /// </summary>
    public enum InstrumentTypeEnum
    {
        /// <summary>
        /// Фьючерсы
        /// </summary>
        [EnumMember(Value = @"Futures")]
        Futures,
        /// <summary>
        /// Акции
        /// </summary>
        [EnumMember(Value = @"Shares")]
        Shares,
        /// <summary>
        /// Опционы
        /// </summary>
        [EnumMember(Value = @"Options")] 
        Options,
        /// <summary>
        /// Облигации
        /// </summary>
        [EnumMember(Value = @"Bonds")] 
        Bonds,
        /// <summary>
        /// Валюты
        /// </summary>
        [EnumMember(Value = @"Currencies")] 
        Currencies

    }
}
