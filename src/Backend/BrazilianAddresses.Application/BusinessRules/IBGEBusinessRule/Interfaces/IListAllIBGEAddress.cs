using BrazilianAddresses.Communication.Requests;
using BrazilianAddresses.Communication.Responses;

namespace BrazilianAddresses.Application.BusinessRules.IBGEBusinessRule.Interfaces
{
    public interface IListAllIBGEAddress
    {
        public Task<List<IBGEResponseJson>> Execute(PaginationBaseRequestJson pagination);
    }
}