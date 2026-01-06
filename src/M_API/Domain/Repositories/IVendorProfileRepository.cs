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

    }
}
