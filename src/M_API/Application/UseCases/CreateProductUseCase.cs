using Application.DTOs;
using Application.Services;
using Domain.Entities;
using Domain.Repositories;

namespace Application.UseCases
{
    public class CreateProductUseCase
    {
        private readonly IProductRepository _productRepository;
        private readonly IVendorProfileRepository _vendorRepository;
        private readonly IProductStripeRepository _stripeRepo;
        private readonly IStripeProductService _stripeService;

        public CreateProductUseCase(
            IProductRepository productRepository,
            IVendorProfileRepository vendorRepository,
            IProductStripeRepository stripeRepo,
            IStripeProductService stripeService)
        {
            _productRepository = productRepository;
            _vendorRepository = vendorRepository;
            _stripeRepo = stripeRepo;
            _stripeService = stripeService;
        }

        public async Task ExecuteAsync(Guid userId, CreateProductDto dto)
        {
            var vendor = await _vendorRepository.GetByUserIdAsync(userId);
            if (vendor == null)
                throw new Exception("Vendor profile not found.");

            var product = new Product(
                vendor.Id,
                dto.Name,
                dto.Description,
                dto.Price,
                dto.Stock,
                dto.Category
            );

            await _productRepository.AddAsync(product);
            await _productRepository.SaveChangesAsync();

            var (stripeProductId, stripePriceId) =
              await _stripeService.CreateProductAsync(
                  product.Name,
                  product.Description,
                  product.Price
              );

            var productStripe = new ProductStripe(
                product.Id,
                stripeProductId,
                stripePriceId
            );

            await _stripeRepo.AddAsync(productStripe);
            await _stripeRepo.SaveChangesAsync();
        }
    }
}
