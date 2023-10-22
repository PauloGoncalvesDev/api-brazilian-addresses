using BrazilianAddresses.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BrazilianAddresses.Infrastructure.RepositoryAccess
{
    public class BrazilianAddressesContext : DbContext
    {
        public BrazilianAddressesContext(DbContextOptions<BrazilianAddressesContext> options) : base(options) { }

        public DbSet<User> User { get; set; }
        
        public DbSet<IBGE> IBGE { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BrazilianAddressesContext).Assembly);
        }
    }
}
