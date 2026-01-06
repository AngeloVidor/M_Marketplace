using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using M_API.Domain.Repositories;

namespace M_API.Application.UseCases
{
    public class GetCartUseCase
    {
        private readonly ICartRepository _cartRepo;

        public GetCartUseCase(ICartRepository cartRepo)
        {
            _cartRepo = cartRepo;
        }

        public async Task<Cart> ExecuteAsync(Guid userId)
        {
            var cart = await _cartRepo.GetActiveByUserIdAsync(userId)
                       ?? new Cart(userId);
            return cart;
        }
    }
}