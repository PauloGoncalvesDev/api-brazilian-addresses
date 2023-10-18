using BrazilianAddresses.Domain.Entities;

namespace BrazilianAddresses.Domain.Repositories.IBGERepository
{
    public interface IIBGEUpdateOnlyRepository
    {
        void Update(IBGE ibge);

        Task<IBGE> GetIBGEByIBGECodeToUpdate(string ibgeCode);
    }
}
