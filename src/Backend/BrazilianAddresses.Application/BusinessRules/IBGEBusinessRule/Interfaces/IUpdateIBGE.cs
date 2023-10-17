using BrazilianAddresses.Communication.Requests;
using BrazilianAddresses.Communication.Responses;

namespace BrazilianAddresses.Application.BusinessRules.IBGEBusinessRule.Interfaces
{
    public interface IUpdateIBGE
    {
        public Task<IBGEResponseJson> Execute(IBGERequestJson ibgeRequestJson);
    }
}
