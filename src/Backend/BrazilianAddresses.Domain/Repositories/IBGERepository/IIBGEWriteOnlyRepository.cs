using BrazilianAddresses.Domain.Entities;

namespace BrazilianAddresses.Domain.Repositories.IBGERepository
{
    public interface IIBGEWriteOnlyRepository
    {
        Task Add(IBGE ibge);
    }
}
