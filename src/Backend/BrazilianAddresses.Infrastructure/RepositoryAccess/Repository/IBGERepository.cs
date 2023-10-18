using Microsoft.EntityFrameworkCore;
using BrazilianAddresses.Domain.Entities;
using BrazilianAddresses.Domain.Repositories.IBGERepository;

namespace BrazilianAddresses.Infrastructure.RepositoryAccess.Repository
{
    public class IBGERepository : IIBGEReadOnlyRepository, IIBGEWriteOnlyRepository
    {
        private readonly BrazilianAddressesContext _context;

        public IBGERepository(BrazilianAddressesContext context)
        {
            _context = context;
        }

        public async Task Add(IBGE ibge)
        {
            await _context.IBGE.AddAsync(ibge);
        }

        public async Task<IBGE> GetIBGEByIBGECode(string ibgeCode)
        {
            return await _context.IBGE.AsNoTracking()
                .FirstOrDefaultAsync(u => u.IBGECode.Equals(ibgeCode));
        }

        public async Task<List<IBGE>> GetAllIBGEAddress()
        {
            return await _context.IBGE.AsNoTracking().ToListAsync();
        }
    }
}