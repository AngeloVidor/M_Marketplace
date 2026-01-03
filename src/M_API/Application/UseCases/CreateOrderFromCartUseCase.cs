using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using M_API.Domain.Repositories;

namespace M_API.Application.UseCases
{
    public class CreateOrderFromCartUseCase
    {
        private readonly ICartRepository _cartRepo;
        private readonly IOrderRepository _orderRepo;

        public CreateOrderFromCartUseCase(ICartRepository cartRepo, IOrderRepository orderRepo)
        {
            _cartRepo = cartRepo;
            _orderRepo = orderRepo;
        }

        public async Task<Guid> ExecuteAsync(Guid userId)
        {
            var cart = await _cartRepo.GetActiveByUserIdAsync(userId)
                       ?? throw new Exception("Cart not found");

            if (!cart.Items.Any())
                throw new Exception("Cart is empty");

            var order = new Order(userId);

            foreach (var item in cart.Items)
            {
                order.Items.Add(new OrderItem(
                    order.Id,
                    item.ProductId,
                    item.ProductName,
                    item.UnitPrice,
                    item.Quantity
                ));
            }

            order.CalculateTotal();
            cart.Convert();

            await _orderRepo.AddAsync(order);
            await _orderRepo.SaveChangesAsync();

            return order.Id;
        }
    }


}