using Microsoft.EntityFrameworkCore;
using BrazilianAddresses.Domain.Entities;
using BrazilianAddresses.Domain.Repositories.IBGERepository;

namespace BrazilianAddresses.Infrastructure.RepositoryAccess.Repository
{
    public class IBGERepository : IIBGEReadOnlyRepository, IIBGEWriteOnlyRepository, IIBGEUpdateOnlyRepository, IIBGERemoveOnlyRepository
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
            return await _context.IBGE.AsNoTracking().FirstOrDefaultAsync(u => u.IBGECode.Equals(ibgeCode) && u.DeletionDate == null);
        }

        public async Task<IBGE> GetIBGEByIBGECodeToUpdate(string ibgeCode)
        {
            return await _context.IBGE.FirstOrDefaultAsync(u => u.IBGECode.Equals(ibgeCode) && u.DeletionDate == null);
        }

        public void Update(IBGE ibge)
        {
            _context.IBGE.Update(ibge);
        }

        public async Task<List<IBGE>> GetAllIBGEAddress()
        {
            return await _context.IBGE.AsNoTracking().Where(ibge => ibge.DeletionDate == null).ToListAsync();
        }

        public void Remove(IBGE ibge)
        {
            _context.IBGE.Update(ibge);
        }
    }
}