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
            await _context.IBGEs.AddAsync(ibge);
        }
    }
}
