using Domain.Entities;

namespace Domain.Repositories
{
    public interface IVendorProfileRepository
    {
        Task AddAsync(VendorProfile vendor);
        Task SaveChangesAsync();
        Task<VendorProfile?> GetByUserIdAsync(Guid userId);
        Task<VendorProfile?> GetByStripeAccountIdAsync(string stripeAccountId);
        Task<List<VendorProfile>> GetAllWithStripeAccountsAsync();
        Task UpdateAsync(VendorProfile vendor);
        Task DeleteAsync(VendorProfile vendor);
        Task<List<VendorProfile>> GetAllAsync();
        Task<VendorProfile?> GetByIdAsync(Guid id);


    }
}
