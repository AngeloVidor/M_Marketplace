using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using M_API.Domain.Repositories;

namespace M_API.Application.UseCases
{
    public class UpdateItemQuantityUseCase
    {
        private readonly ICartRepository _cartRepo;

        public UpdateItemQuantityUseCase(ICartRepository cartRepo)
        {
            _cartRepo = cartRepo;
        }

        public async Task ExecuteAsync(Guid userId, Guid productId, int quantity)
        {
            var cart = await _cartRepo.GetActiveByUserIdAsync(userId)
                       ?? throw new Exception("Cart not found");

            var item = await _cartRepo.GetItemAsync(cart.Id, productId)
                       ?? throw new Exception("Item not found in cart");

            item.UpdateQuantity(quantity);
            await _cartRepo.SaveChangesAsync();
        }
    }

}