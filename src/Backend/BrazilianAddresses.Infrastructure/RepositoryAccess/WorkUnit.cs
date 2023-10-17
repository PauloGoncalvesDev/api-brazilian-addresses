using BrazilianAddresses.Domain.Repositories;

namespace BrazilianAddresses.Infrastructure.RepositoryAccess
{
    public sealed class WorkUnit : IDisposable, IWorkUnit
    {
        private readonly BrazilianAddressesContext _context;

        private bool _disposed;

        public WorkUnit(BrazilianAddressesContext context)
        {
            _context = context;
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _context.Dispose();
            }

            _disposed = true;
        }
    }
}
