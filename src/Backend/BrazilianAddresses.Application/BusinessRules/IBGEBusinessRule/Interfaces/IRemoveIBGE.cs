using BrazilianAddresses.Communication.Requests;
using BrazilianAddresses.Communication.Responses;

namespace BrazilianAddresses.Application.BusinessRules.IBGEBusinessRule.Interfaces
{
    public interface IRemoveIBGE
    {
        public Task<IBGEResponseJson> Execute(string iBGECode);
    }
}
