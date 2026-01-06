using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;

namespace M_API.Application.UseCases
{
    public class GetProductsUseCase
    {
        private readonly IProductRepository _productRepository;
        private readonly IVendorProfileRepository _vendorRepository;

        public GetProductsUseCase(IProductRepository productRepository, IVendorProfileRepository vendorRepository)
        {
            _productRepository = productRepository;
            _vendorRepository = vendorRepository;
        }

        public async Task<IEnumerable<Product>> ExecuteAsync(Guid userId)
        {
            var vendor = await _vendorRepository.GetByUserIdAsync(userId);
            if (vendor == null) throw new Exception("Vendor not found.");

            return await _productRepository.GetByVendorIdAsync(vendor.Id);
        }
    }
}