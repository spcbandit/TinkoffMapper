using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TinkoffMapper.Requests
{
    internal class Request : IRequestContent
    {
        [NotNull]
        private readonly RequestInvest _requestInvest;
        private readonly string _apiToken;
        private readonly string _accountId;
        private readonly bool _sandboxMode;
        public Request([NotNull] RequestInvest requestInvest, [CanBeNull] string apiToken, [CanBeNull] string accountId, bool sandboxMode = false)
        {
            _requestInvest = requestInvest ?? throw new ArgumentNullException(nameof(requestInvest));
            _apiToken = apiToken;
            _accountId = accountId;
            _sandboxMode = sandboxMode;
        }

        public string Query
        {
            get
            {
                if (_sandboxMode && !string.IsNullOrEmpty(_requestInvest.SandboxEndPoint))
                    return _requestInvest.SandboxEndPoint;

                return _requestInvest.EndPoint;
            }
        }
        public RequestMethod Method => _requestInvest.Method;

        public virtual IReadOnlyDictionary<string, string> Headers => null;

        public object Body => _requestInvest.Body;

        [NotNull]
        protected virtual string GenerateParametersString([NotNull] IDictionary<string, string> properties) =>
            string.Join("&", properties.Select(a => $"{a.Key}={a.Value}"));
    }
}
