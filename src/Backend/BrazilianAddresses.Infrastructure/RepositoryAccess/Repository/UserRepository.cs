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
            await _context.User.AddAsync(user);
        }

        public async Task<bool> ExistsUserWithEmail(string email)
        {
            return await _context.User.AnyAsync(user => user.Email.Equals(email) && user.DeletionDate == null);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.User.AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email.Equals(email));
        }

        public async Task<User> GetUserLogin(string email, string password)
        {
            return await _context.User.AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email.Equals(email) && u.Password.Equals(password));
        }
    }
}
