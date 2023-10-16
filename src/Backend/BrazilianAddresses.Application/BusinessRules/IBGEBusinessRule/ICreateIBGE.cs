using BrazilianAddresses.Communication.Requests;

namespace BrazilianAddresses.Application.BusinessRules.IBGEBusinessRule
{
    public interface ICreateIBGE
    {
        public Task Execute(IBGERequestJson iBGERequestJson);
    }
}
