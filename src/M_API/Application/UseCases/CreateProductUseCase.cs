using Application.DTOs;
using Domain.Entities;
using Domain.Repositories;

namespace Application.UseCases
{
    public class CreateProductUseCase
    {
        private readonly IProductRepository _productRepository;
        private readonly IVendorProfileRepository _vendorRepository;

        public CreateProductUseCase(
            IProductRepository productRepository,
            IVendorProfileRepository vendorRepository)
        {
            _productRepository = productRepository;
            _vendorRepository = vendorRepository;
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
        }
    }
}
