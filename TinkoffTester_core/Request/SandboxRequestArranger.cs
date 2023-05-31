using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinkoffMapper.Requests;

namespace TinkoffTester.Request
{
    public sealed class SandboxRequestArranger
    {

        /// <summary>
        /// Пустой конструктор для создания публичных запросов из (Payload)
        /// </summary>
        public SandboxRequestArranger()
        { }
        /// <summary>
        /// Конструктор для создания приватных запросов из (Payload)
        /// </summary>
        /// <param name="apiToken">Публичный ключ аккаунта</param>
        /// <param name="accountId">Приватный ключ аккаунта</param>
        public SandboxRequestArranger([NotNull] string apiToken, [CanBeNull] string accountId)
        {
            if (string.IsNullOrWhiteSpace(apiToken))
                throw new ArgumentException("Cannot be empty!", nameof(apiToken));

            ApiToken = apiToken;
            AccountId = accountId;
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
        /// Метод создания запроса к бирже
        /// </summary>
        /// <param name="payload">Вся собранная информация запроса</param>
        /// <returns></returns>
        [NotNull]
        public IRequestContent Arrange([NotNull] SandboxRequestInvest payload)
        {
            return new SandboxRequest(payload, ApiToken, AccountId);
        }
    }
}
