using BrazilianAddresses.Domain.Entities;
using BrazilianAddresses.Domain.Repositories.UserRepository;
using Microsoft.EntityFrameworkCore;

namespace BrazilianAddresses.Infrastructure.RepositoryAccess.Repository
{
    public class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository
    {
        private readonly BrazilianAddressesContext _context;

        public UserRepository(BrazilianAddressesContext context)
        {
            _context = context;
        }

        public async Task Add(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<bool> ExistsUserWithEmail(string email)
        {
            return await _context.Users.AnyAsync(user => user.Email.Equals(email));
        }
    }
}
