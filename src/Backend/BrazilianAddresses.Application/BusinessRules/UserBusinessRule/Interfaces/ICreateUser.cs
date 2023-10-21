using BrazilianAddresses.Communication.Requests;
using BrazilianAddresses.Communication.Responses;

namespace BrazilianAddresses.Application.BusinessRules.UserBusinessRule.Interfaces
{
    public interface ICreateUser
    {
        public Task<BaseResponseJson> Execute(UserRequestJson userRequestJson);
    }
}
