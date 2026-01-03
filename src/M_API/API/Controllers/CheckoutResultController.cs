using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace M_API.API.Controllers
{
    [ApiController]
    public class CheckoutResultController : ControllerBase
    {
        [HttpGet("/success")]
        public IActionResult Success()
        {
            return Ok(new
            {
                message = "Payment successful. Your order is being processed."
            });
        }

        [HttpGet("/cancel")]
        public IActionResult Cancel()
        {
            return Ok(new
            {
                message = "Payment canceled."
            });
        }
    }
}