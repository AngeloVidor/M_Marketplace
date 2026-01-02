using Domain.Entities;

namespace Domain.Repositories
{
    public interface ICustomerProfileRepository
    {
        Task AddAsync(CustomerProfile profile);
        Task<CustomerProfile?> GetByIdAsync(Guid id);
        Task SaveChangesAsync();
    }
}
