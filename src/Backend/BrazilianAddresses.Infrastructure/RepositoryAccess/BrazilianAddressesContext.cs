using BrazilianAddresses.Domain.Entitys;
using Microsoft.EntityFrameworkCore;

namespace BrazilianAddresses.Infrastructure.RepositoryAccess
{
    public class BrazilianAddressesContext : DbContext
    {
        public BrazilianAddressesContext(DbContextOptions<BrazilianAddressesContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        
        public DbSet<IBGE> IBGEs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BrazilianAddressesContext).Assembly);
        }
    }
}
