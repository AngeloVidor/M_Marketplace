using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class VendorProfileRepository : IVendorProfileRepository
    {
        private readonly AppDbContext _context;

        public VendorProfileRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(VendorProfile vendor)
        {
            if (vendor == null)
                throw new ArgumentNullException(nameof(vendor));

            await _context.VendorProfiles.AddAsync(vendor);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<VendorProfile?> GetByUserIdAsync(Guid userId)
        {
            return await _context.VendorProfiles
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.UserId == userId);
        }

        public async Task<VendorProfile?> GetByStripeAccountIdAsync(string stripeAccountId)
        {
            return await _context.VendorProfiles
                .FirstOrDefaultAsync(v =>
                    v.StripeAccountId != null &&
                    v.StripeAccountId.ToLower() == stripeAccountId.ToLower());
        }

        public async Task<List<VendorProfile>> GetAllWithStripeAccountsAsync()
        {
            return await _context.VendorProfiles
                .Where(v => v.StripeAccountId != null)
                .ToListAsync();
        }

        public async Task UpdateAsync(VendorProfile vendor)
        {
            _context.VendorProfiles.Update(vendor);
        }

        public async Task DeleteAsync(VendorProfile vendor)
        {
            _context.VendorProfiles.Remove(vendor);
        }

        public async Task<List<VendorProfile>> GetAllAsync()
        {
            return await _context.VendorProfiles.ToListAsync();
        }

        public async Task<VendorProfile?> GetByIdAsync(Guid id)
        {
            return await _context.VendorProfiles.FindAsync(id);
        }


    }
}
