using BrazilianAddresses.Domain.Entitys;

namespace BrazilianAddresses.Domain.Repositories
{
    public interface IIBGEWriteOnlyRepository
    {
        Task Add(IBGE ibge);
    }
}
