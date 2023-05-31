
namespace TinkoffMapper.Invest.Stream.Data
{
    public class StreamErrorMessage
    {
        /// <summary>
        /// Сообщение об ошибке
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Является ли ошибка системной
        /// </summary>
        public bool IsSystem { get; set; }

        public StreamErrorMessage(string message, bool isSystem = false)
        {
            Message = message;
            IsSystem = isSystem;
        }
    }
}
