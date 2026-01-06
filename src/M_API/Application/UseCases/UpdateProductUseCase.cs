using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs;
using Domain.Repositories;

namespace M_API.Application.UseCases
{
    public class UpdateProductUseCase
    {
        private readonly IProductRepository _productRepository;
        private readonly IVendorProfileRepository _vendorRepository;

        public UpdateProductUseCase(IProductRepository productRepository, IVendorProfileRepository vendorRepository)
        {
            _productRepository = productRepository;
            _vendorRepository = vendorRepository;
        }

        public async Task ExecuteAsync(Guid userId, Guid productId, CreateProductDto dto)
        {
            var vendor = await _vendorRepository.GetByUserIdAsync(userId);
            if (vendor == null) throw new Exception("Vendor not found.");

            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null || product.VendorProfileId != vendor.Id)
                throw new Exception("Product not found or access denied.");

            product.Update(dto.Name, dto.Description, dto.Price, dto.Stock, dto.Category);

            await _productRepository.UpdateAsync(product);
            await _productRepository.SaveChangesAsync();
        }
    }

}