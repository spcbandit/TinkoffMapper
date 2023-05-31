using TinkoffMapper.Requests;
using TinkoffTester.Request;

namespace TinkoffTester.Sandbox.Request
{
    public class OpenSandboxAccountRequest : SandboxRequestInvest
    {
        internal override string EndPoint => "/tinkoff.public.invest.api.contract.v1.SandboxService/OpenSandboxAccount";
        internal override RequestMethod Method => RequestMethod.POST;
        internal override object Body => null;
    }
}
