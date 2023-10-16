using BrazilianAddresses.Domain.Entities;

namespace BrazilianAddresses.Domain.Repositories
{
    public interface IIBGEWriteOnlyRepository
    {
        Task Add(IBGE ibge);
    }
}
