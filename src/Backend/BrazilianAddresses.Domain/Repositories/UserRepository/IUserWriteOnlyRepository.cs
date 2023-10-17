using BrazilianAddresses.Domain.Entities;

namespace BrazilianAddresses.Domain.Repositories.UserRepository
{
    public interface IUserWriteOnlyRepository
    {
        Task Add(User user);
    }
}
