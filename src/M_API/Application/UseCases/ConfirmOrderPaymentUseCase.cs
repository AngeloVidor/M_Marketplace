using System;
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
        private readonly IPurchaseHistoryRepository _historyRepo;

        public ConfirmOrderPaymentUseCase(
            IOrderRepository orderRepo,
            IProductRepository productRepo,
            IPurchaseHistoryRepository historyRepo)
        {
            _orderRepo = orderRepo;
            _productRepo = productRepo;
            _historyRepo = historyRepo;
        }

        public async Task ExecuteAsync(Session session)
        {
            if (!session.Metadata.TryGetValue("orderId", out var orderIdStr) || !Guid.TryParse(orderIdStr, out var orderId))
                throw new Exception("Invalid or missing orderId in session metadata");

            var order = await _orderRepo.GetByIdAsync(orderId)
                ?? throw new Exception("Order not found");

            foreach (var item in order.Items)
            {
                var product = await _productRepo.GetByIdAsync(item.ProductId)
                              ?? throw new Exception($"Product with ID {item.ProductId} not found");

                product.DecreaseStock(item.Quantity);

                var purchase = new PurchaseHistory(
                    order.UserId,
                    product.Id,
                    product.Name,
                    item.Quantity,
                    product.Price
                );

                await _historyRepo.AddAsync(purchase);
            }

            order.MarkAsPaid();

            await _orderRepo.SaveChangesAsync();
            await _historyRepo.SaveChangesAsync();
        }
    }
}
