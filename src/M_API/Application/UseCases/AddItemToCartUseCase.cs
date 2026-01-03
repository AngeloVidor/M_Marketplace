using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using M_API.Domain.Repositories;

namespace M_API.Application.UseCases
{
    public class AddItemToCartUseCase
    {
        private readonly ICartRepository _cartRepo;
        private readonly IProductRepository _productRepo;

        public AddItemToCartUseCase(ICartRepository cartRepo, IProductRepository productRepo)
        {
            _cartRepo = cartRepo;
            _productRepo = productRepo;
        }

        public async Task ExecuteAsync(Guid userId, Guid productId, int quantity)
        {
            var cart = await _cartRepo.GetActiveByUserIdAsync(userId);
            var isNewCart = false;

            if (cart == null)
            {
                cart = new Cart(userId);
                isNewCart = true;
            }

            var product = await _productRepo.GetByIdAsync(productId)
                          ?? throw new Exception("Product not found");

            var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (existingItem != null)
            {
                existingItem.IncreaseQuantity(quantity);
            }
            else
            {
                var newItem = new CartItem(
                    cart.Id,
                    product.Id,
                    product.Name,
                    product.Price,
                    quantity
                );

                cart.Items.Add(newItem);

                _cartRepo.AddItem(newItem);
            }

            if (isNewCart)
                await _cartRepo.AddAsync(cart);

            await _cartRepo.SaveChangesAsync();
        }
    }
}
