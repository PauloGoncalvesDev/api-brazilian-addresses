using BrazilianAddresses.Domain.Entities;

namespace BrazilianAddresses.Domain.Repositories
{
    public interface IUserWriteOnlyRepository
    {
        Task Add(User user);
    }
}
