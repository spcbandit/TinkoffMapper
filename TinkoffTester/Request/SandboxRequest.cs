using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinkoffMapper.Requests;

namespace TinkoffTester.Request
{
    internal class SandboxRequest : IRequestContent
    {
        [NotNull]
        private readonly SandboxRequestInvest _requestInvest;
        private readonly string _apiToken;
        private readonly string _accountId;
        public SandboxRequest([NotNull] SandboxRequestInvest requestInvest, [CanBeNull] string apiToken, [CanBeNull] string accountId)
        {
            _requestInvest = requestInvest ?? throw new ArgumentNullException(nameof(requestInvest));
            _apiToken = apiToken;
            _accountId = accountId;
        }

        public string Query
        {
            get
            {
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
