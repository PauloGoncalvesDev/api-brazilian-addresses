using BrazilianAddresses.Domain.Entitys;

namespace BrazilianAddresses.Domain.Repositories
{
    public interface IUserWriteOnlyRepository
    {
        Task Add(User user);
    }
}
