using BrazilianAddresses.Communication.Requests;
using BrazilianAddresses.Communication.Responses;

namespace BrazilianAddresses.Application.BusinessRules.IBGEBusinessRule.Interfaces
{
    public interface ICreateIBGE
    {
        public Task<IBGEResponseJson> Execute(IBGERequestJson iBGERequestJson);
    }
}
