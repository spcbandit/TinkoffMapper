using TinkoffMapper.Requests;

namespace TinkoffMapper.Invest.Rest.Common.Request.Account
{
    public class GetAccountsRequest : RequestInvest
    {
        //https://tinkoff.github.io/investAPI/users/#getaccounts
        /// <summary>
        /// Запрос получения счетов пользователя
        /// </summary>
        public GetAccountsRequest()
        {
            
        }
        internal override string EndPoint => "/tinkoff.public.invest.api.contract.v1.UsersService/GetAccounts";
        internal override string SandboxEndPoint => "tinkoff.public.invest.api.contract.v1.SandboxService/GetSandboxAccounts";

        internal override RequestMethod Method => RequestMethod.POST;
        internal override object Body => null;
    }
}
