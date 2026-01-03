using Stripe;
using Application.Services;
using Infrastructure.Payments.Stripe;

namespace Infrastructure.Services
{
    public class StripeProductService : IStripeProductService
    {
        private readonly ProductService _productService;
        private readonly PriceService _priceService;
        private readonly StripeSettings _stripeSettings;
        public StripeProductService(ProductService productService, PriceService priceService, StripeSettings stripeSettings)
        {
            _productService = productService;
            _priceService = priceService;
            _stripeSettings = stripeSettings;
        }

        public async Task<(string productId, string priceId)> CreateProductAsync(string name, string description, decimal price)
        {
            var stripeProduct = await _productService.CreateAsync(new ProductCreateOptions
            {
                Name = name,
                Description = description
            });

            var stripePrice = await _priceService.CreateAsync(new PriceCreateOptions
            {
                Product = stripeProduct.Id,
                UnitAmount = (long)(price * 100),
                Currency = "brl"
            });

            return (stripeProduct.Id, stripePrice.Id);
        }
    }
}
