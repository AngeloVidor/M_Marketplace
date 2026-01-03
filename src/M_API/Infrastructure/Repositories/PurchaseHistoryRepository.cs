using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PurchaseHistoryRepository : IPurchaseHistoryRepository
    {
        private readonly AppDbContext _context;

        public PurchaseHistoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(PurchaseHistory purchase)
            => await _context.PurchaseHistories.AddAsync(purchase);

        public async Task<IEnumerable<PurchaseHistory>> GetByUserIdAsync(Guid userId)
            => await _context.PurchaseHistories
                .AsNoTracking()
                .Where(p => p.UserId == userId)
                .ToListAsync();

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}
