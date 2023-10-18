using BrazilianAddresses.Domain.Entities;

namespace BrazilianAddresses.Domain.Repositories.IBGERepository
{
    public interface IIBGERemoveOnlyRepository
    {
        void Remove(IBGE ibge);
    }
}
