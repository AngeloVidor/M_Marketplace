using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs;
using Domain.Repositories;

namespace M_API.Application.UseCases
{

    public class GetSellerSalesUseCase
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IProductRepository _productRepo;

        public GetSellerSalesUseCase(IOrderRepository orderRepo, IProductRepository productRepo)
        {
            _orderRepo = orderRepo;
            _productRepo = productRepo;
        }

        public async Task<VendorSalesDto> ExecuteAsync(Guid vendorProfileId)
        {
            var orders = await _orderRepo.GetAllPaidOrdersAsync();

            int totalItems = 0;
            decimal totalRevenue = 0;
            int itemsThisMonth = 0;
            int itemsThisYear = 0;

            var now = DateTime.UtcNow;

            foreach (var order in orders)
            {
                foreach (var item in order.Items)
                {
                    var product = await _productRepo.GetByIdAsync(item.ProductId);
                    if (product == null || product.VendorProfileId != vendorProfileId)
                        continue;

                    totalItems += item.Quantity;
                    totalRevenue += item.Subtotal;

                    if (order.CreatedAt.Month == now.Month && order.CreatedAt.Year == now.Year)
                        itemsThisMonth += item.Quantity;

                    if (order.CreatedAt.Year == now.Year)
                        itemsThisYear += item.Quantity;
                }
            }

            return new VendorSalesDto(
                vendorProfileId,
                totalItems,
                itemsThisMonth,
                itemsThisYear,
                totalRevenue
            );
        }
    }

}