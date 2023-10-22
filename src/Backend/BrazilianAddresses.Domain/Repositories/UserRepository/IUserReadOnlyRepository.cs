using BrazilianAddresses.Domain.Entities;

namespace BrazilianAddresses.Domain.Repositories.UserRepository
{
    public interface IUserReadOnlyRepository
    {
        Task<bool> ExistsUserWithEmail(string email);

        Task<User> GetUserLogin(string email, string password);

        Task<User> GetUserByEmail(string email);
    }
}
