using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CustomerProfileRepository : ICustomerProfileRepository
    {
        private readonly AppDbContext _context;

        public CustomerProfileRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(CustomerProfile profile)
        {
            await _context.CustomerProfiles.AddAsync(profile);
        }

        public async Task<CustomerProfile?> GetByIdAsync(Guid id)
        {
            return await _context.CustomerProfiles.FindAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
