using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;

namespace M_API.Application.UseCases
{
    public class GetProductByIdUseCase
    {
        private readonly IProductRepository _productRepository;
        private readonly IVendorProfileRepository _vendorRepository;

        public GetProductByIdUseCase(IProductRepository productRepository, IVendorProfileRepository vendorRepository)
        {
            _productRepository = productRepository;
            _vendorRepository = vendorRepository;
        }

        public async Task<Product> ExecuteAsync(Guid userId, Guid productId)
        {
            var vendor = await _vendorRepository.GetByUserIdAsync(userId);
            if (vendor == null) throw new Exception("Vendor not found.");

            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null || product.VendorProfileId != vendor.Id)
                throw new Exception("Product not found or access denied.");

            return product;
        }
    }
}