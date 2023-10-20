using BrazilianAddresses.Domain.Entities;

namespace BrazilianAddresses.Domain.Repositories.IBGERepository
{
    public interface IIBGEReadOnlyRepository
    {
        Task<IBGE> GetIBGEByIBGECode(string ibgeCode);

        Task<List<IBGE>> GetAllIBGEAddresses();

        Task<List<IBGE>> GetIBGEAddressesByState(string state);
    }
}