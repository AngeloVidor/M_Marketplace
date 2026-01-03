using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Repositories;
using Stripe.Checkout;

namespace M_API.Application.UseCases
{
    public class ConfirmOrderPaymentUseCase
    {
        private readonly IOrderRepository _orderRepo;

        public ConfirmOrderPaymentUseCase(IOrderRepository orderRepo)
        {
            _orderRepo = orderRepo;
        }

        public async Task ExecuteAsync(Session session)
        {
            var orderId = Guid.Parse(session.Metadata["orderId"]);

            var order = await _orderRepo.GetByIdAsync(orderId)
                ?? throw new Exception("Order not found");

            order.MarkAsPaid();
            await _orderRepo.SaveChangesAsync();
        }
    }
}