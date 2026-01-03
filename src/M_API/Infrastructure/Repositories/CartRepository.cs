using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Data;
using M_API.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace M_API.Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _context;

        public CartRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Cart?> GetActiveByUserIdAsync(Guid userId)
        {
            return await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId == userId && !c.IsConverted);
        }
        public void AddItem(CartItem item)
        {
            _context.CartItems.Add(item);
        }
        
        public async Task AddAsync(Cart cart)
            => await _context.Carts.AddAsync(cart);

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }

}