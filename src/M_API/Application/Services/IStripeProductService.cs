namespace Application.Services
{
    public interface IStripeProductService
    {
        Task<(string productId, string priceId)> CreateProductAsync(
            string name,
            string description,
            decimal price
        );
    }
}