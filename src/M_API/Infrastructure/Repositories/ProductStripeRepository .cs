using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProductStripeRepository : IProductStripeRepository
    {
        private readonly AppDbContext _context;

        public ProductStripeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ProductStripe productStripe)
        {
            await _context.ProductStripes.AddAsync(productStripe);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<ProductStripe?> GetByProductIdAsync(Guid productId)
        {
            return await _context.ProductStripes
                .FirstOrDefaultAsync(ps => ps.ProductId == productId);
        }
    }
}
