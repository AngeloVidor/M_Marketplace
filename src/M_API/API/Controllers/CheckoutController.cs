using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace M_API.API.Controllers
{
    [ApiController]
    [Route("api/checkout")]
    [Authorize]
    public class CheckoutController : ControllerBase
    {
        private readonly CreateCheckoutSessionUseCase _useCase;

        public CheckoutController(CreateCheckoutSessionUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpPost]
        public async Task<IActionResult> Checkout([FromBody] Guid orderId)
        {
            var url = await _useCase.ExecuteAsync(orderId);
            return Ok(new { checkoutUrl = url });
        }
    }

}