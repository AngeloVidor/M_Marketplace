using Domain.Entities;

namespace Domain.Repositories
{
    public interface IProductRepository
    {
        Task AddAsync(Product product);
        Task SaveChangesAsync();
    }
}
