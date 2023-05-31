
namespace TinkoffMapper.Invest.Stream.Data
{
    public class StreamCloseMessage
    {
        /// <summary>
        /// Причина
        /// </summary>
        public string Reason { get; set; }
        /// <summary>
        /// Нормальная причина закрытия или ошибка
        /// </summary>
        public bool IsNormal { get; set; }

        public StreamCloseMessage(bool isNormal = true, string reason = "")
        {
            Reason = reason;
            IsNormal = isNormal;
        }
    }
}
