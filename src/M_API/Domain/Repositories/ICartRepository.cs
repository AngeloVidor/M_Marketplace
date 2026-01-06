using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace M_API.Domain.Repositories
{
    public interface ICartRepository
    {
        Task<Cart?> GetActiveByUserIdAsync(Guid userId);
        void AddItem(CartItem item);
        void RemoveItem(CartItem item);
        Task<CartItem?> GetItemAsync(Guid cartId, Guid productId);
        Task AddAsync(Cart cart);
        Task UpdateAsync(Cart cart);
        Task DeleteAsync(Cart cart);
        Task SaveChangesAsync();
    }

}