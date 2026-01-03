using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Security;
using M_API.Application.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace M_API.API.Controllers
{
    [ApiController]
    [Route("api/orders")]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly CreateOrderFromCartUseCase _createOrderFromCart;

        public OrdersController(CreateOrderFromCartUseCase createOrderFromCart)
        {
            _createOrderFromCart = createOrderFromCart;
        }

        [HttpPost("from-cart")]
        public async Task<IActionResult> CreateFromCart()
        {
            var userId = ClaimsHelper.GetUserId(User);

            var orderId = await _createOrderFromCart.ExecuteAsync(userId);

            return Ok(new { orderId });
        }
    }
}