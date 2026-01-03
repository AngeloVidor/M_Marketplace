using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using Stripe.Checkout;

namespace M_API.Application.UseCases
{
    public class ConfirmOrderPaymentUseCase
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IProductRepository _productRepo;

        public ConfirmOrderPaymentUseCase(IOrderRepository orderRepo, IProductRepository productRepo)
        {
            _orderRepo = orderRepo;
            _productRepo = productRepo;
        }

        public async Task ExecuteAsync(Session session)
        {
            var orderId = Guid.Parse(session.Metadata["orderId"]);

            var order = await _orderRepo.GetByIdAsync(orderId)
                ?? throw new Exception("Order not found");

            foreach (var item in order.Items)
            {
                var product = await _productRepo.GetByIdAsync(item.ProductId) ?? throw new Exception($"Product with ID {item.ProductId} not found");
                product.DecreaseStock(item.Quantity);
            }

            order.MarkAsPaid();

            await _orderRepo.SaveChangesAsync();
        }
    }
}