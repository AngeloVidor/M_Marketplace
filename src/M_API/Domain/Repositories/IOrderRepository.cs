using Domain.Entities;

namespace Domain.Repositories
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
        Task<Order?> GetByIdAsync(Guid id);
        Task<IEnumerable<Order>> GetByUserIdAsync(Guid userId);
        Task SaveChangesAsync();
    }
}
