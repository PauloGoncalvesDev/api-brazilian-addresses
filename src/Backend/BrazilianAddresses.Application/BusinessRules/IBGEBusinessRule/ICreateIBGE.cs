using BrazilianAddresses.Communication.Requests;
using BrazilianAddresses.Communication.Responses;

namespace BrazilianAddresses.Application.BusinessRules.IBGEBusinessRule
{
    public interface ICreateIBGE
    {
        public Task<IBGEResponseJson> Execute(IBGERequestJson iBGERequestJson);
    }
}
