namespace BrazilianAddresses.Domain.Repositories.UserRepository
{
    public interface IUserReadOnlyRepository
    {
        Task<bool> ExistsUserWithEmail(string email);
    }
}
