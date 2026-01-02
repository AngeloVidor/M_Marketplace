using Domain.Entities;
using Infrastructure.Data;
using M_API.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ActivationTokenRepository : IActivationTokenRepository
    {
        private readonly AppDbContext _context;

        public ActivationTokenRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ActivationToken token)
        {
            await _context.ActivationTokens.AddAsync(token);
        }

        public async Task<ActivationToken?> GetByTokenAsync(string token)
        {
            return await _context.ActivationTokens
                .FirstOrDefaultAsync(t => t.Token == token);
        }

        public async Task RemoveAsync(ActivationToken token)
        {
            _context.ActivationTokens.Remove(token);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
