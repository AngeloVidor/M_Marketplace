using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IPurchaseHistoryRepository
    {
        Task AddAsync(PurchaseHistory purchase);
        Task<IEnumerable<PurchaseHistory>> GetByUserIdAsync(Guid userId);
        Task SaveChangesAsync();
    }
}
