using BrazilianAddresses.Communication.Requests;
using BrazilianAddresses.Communication.Responses;

namespace BrazilianAddresses.Application.BusinessRules.IBGEBusinessRule.Interfaces
{
    public interface IListAllIBGEAddresses
    {
        public Task<List<AddressResponseJson>> Execute(PaginationBaseRequestJson pagination);
    }
}