using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data;
using M_API.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PendingRegistrationRepository : IPendingRegistrationRepository
    {
        private readonly AppDbContext _context;

        public PendingRegistrationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(PendingRegistration pending)
        {
            await _context.PendingRegistrations.AddAsync(pending);
        }

        public async Task<PendingRegistration?> GetByEmailAsync(string email)
        {
            return await _context.PendingRegistrations
                .Include(p => p.ActivationToken)
                .FirstOrDefaultAsync(p => p.Email.Value == email);
        }

        public async Task<PendingRegistration?> GetByTokenAsync(string token)
        {
            return await _context.PendingRegistrations
                .Include(p => p.ActivationToken)
                .FirstOrDefaultAsync(p => p.ActivationToken.Token == token);
        }

        public async Task RemoveAsync(PendingRegistration pending)
        {
            _context.PendingRegistrations.Remove(pending);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
