using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using M_API.Domain.Repositories;

namespace M_API.Application.UseCases
{
    public class RemoveItemFromCartUseCase
    {
        private readonly ICartRepository _cartRepo;

        public RemoveItemFromCartUseCase(ICartRepository cartRepo)
        {
            _cartRepo = cartRepo;
        }

        public async Task ExecuteAsync(Guid userId, Guid productId)
        {
            var cart = await _cartRepo.GetActiveByUserIdAsync(userId)
                       ?? throw new Exception("Cart not found");

            var item = await _cartRepo.GetItemAsync(cart.Id, productId)
                       ?? throw new Exception("Item not found in cart");

            _cartRepo.RemoveItem(item);
            await _cartRepo.SaveChangesAsync();
        }
    }

}