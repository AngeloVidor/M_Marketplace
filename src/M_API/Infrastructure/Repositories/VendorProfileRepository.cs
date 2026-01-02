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
    }
}
