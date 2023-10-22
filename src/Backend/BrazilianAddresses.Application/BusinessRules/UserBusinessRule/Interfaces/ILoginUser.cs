using BrazilianAddresses.Communication.Requests;
using BrazilianAddresses.Communication.Responses;

namespace BrazilianAddresses.Application.BusinessRules.UserBusinessRule.Interfaces
{
    public interface ILoginUser
    {
        public Task<UserLoginResponseJson> Execute(UserLoginRequestJson userLoginRequest);
    }
}
