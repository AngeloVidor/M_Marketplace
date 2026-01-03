using Domain.Entities;

public interface IProductStripeRepository
{
    Task AddAsync(ProductStripe productStripe);
    Task SaveChangesAsync();
}
