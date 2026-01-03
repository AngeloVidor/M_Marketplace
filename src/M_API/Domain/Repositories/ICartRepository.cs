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
        Task AddAsync(Cart cart);
        Task SaveChangesAsync();
    }

}