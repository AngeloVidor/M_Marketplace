using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace M_API.Domain.Repositories
{
    public interface ICartItemRepository
    {
        Task AddAsync(CartItem item);
        Task<CartItem?> GetByIdAsync(Guid id);
        Task RemoveAsync(CartItem item);
        Task SaveChangesAsync();
    }

}