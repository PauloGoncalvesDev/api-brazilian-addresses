using BrazilianAddresses.Domain.Entities;

namespace BrazilianAddresses.Domain.Repositories.IBGERepository
{
    public interface IIBGEReadOnlyRepository
    {
        Task<bool> GetExistingIBGEByIBGECode(string ibgeCode);
    }
}
