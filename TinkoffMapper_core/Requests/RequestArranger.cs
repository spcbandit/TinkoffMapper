using JetBrains.Annotations;
using System;

namespace TinkoffMapper.Requests
{
    public sealed class RequestArranger
    {
        /// <summary>
        /// Пустой конструктор для создания публичных запросов из (Payload)
        /// </summary>
        public RequestArranger()
        { }

        /// <summary>
        /// Конструктор для создания приватных запросов из (Payload)
        /// </summary>
        /// <param name="apiToken">Публичный ключ аккаунта</param>
        /// <param name="accountId">Id аккаунта</param>
        /// <param name="sandboxMode">Режим песочницы</param>
        public RequestArranger([NotNull] string apiToken, [CanBeNull] string accountId, [CanBeNull] bool sandboxMode = false)
        {
            if (string.IsNullOrWhiteSpace(apiToken))
                throw new ArgumentException("Cannot be empty!", nameof(apiToken));

            ApiToken = apiToken;
            AccountId = accountId;
            SandboxMode = sandboxMode;
        }

        /// <summary>
        /// Токен доступа для авторизации
        /// </summary>
        public string ApiToken { get; set; }
        
        /// <summary>
        /// Номер счёта
        /// </summary>
        public string AccountId { get; set; }

        /// <summary>
        /// Режим управления песочницей
        /// </summary>
        public bool SandboxMode { get; set; }

        /// <summary>
        /// Метод создания запроса к бирже
        /// </summary>
        /// <param name="payload">Вся собранная информация запроса</param>
        /// <returns></returns>
        [NotNull]
        public IRequestContent Arrange([NotNull] RequestInvest payload)
        {
            return new Request(payload, ApiToken, AccountId, SandboxMode);
        }
    }
}
