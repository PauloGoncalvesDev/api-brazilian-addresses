﻿using BrazilianAddresses.Domain.Entities;
using BrazilianAddresses.Domain.Repositories.IBGERepository;
using Microsoft.EntityFrameworkCore;

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

        public async Task<bool> GetExistingIBGEByIBGECode(string ibgeCode)
        {
            return await _context.IBGE.AnyAsync(u => u.IBGECode.Equals(ibgeCode));
        }
    }
}
