using JetBrains.Annotations;

namespace TinkoffMapper.Requests
{
    public sealed class RequestArranger
    {
        /// <summary>
        /// Конструктор для создания приватных запросов из (Payload)
        /// </summary>
        /// <param name="apiToken">Публичный ключ аккаунта</param>
        /// <param name="sandboxMode">Режим песочницы</param>
        public RequestArranger([CanBeNull] string apiToken = null, [CanBeNull] bool sandboxMode = false)
        {
            ApiToken = apiToken;
            SandboxMode = sandboxMode;
        }

        /// <summary>
        /// Токен доступа для авторизации
        /// </summary>
        public string ApiToken { get; set; }

        /// <summary>
        /// Режим управления песочницей
        /// </summary>
        public bool SandboxMode { get; private set; }

        /// <summary>
        /// Метод создания запроса к бирже
        /// </summary>
        /// <param name="payload">Вся собранная информация запроса</param>
        /// <returns></returns>
        [NotNull]
        public IRequestContent Arrange([NotNull] RequestInvest payload)
        {
            return new Request(payload, ApiToken, SandboxMode);
        }
    }
}
