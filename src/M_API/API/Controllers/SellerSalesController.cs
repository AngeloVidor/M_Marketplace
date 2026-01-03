using Microsoft.AspNetCore.Mvc;
using Application.UseCases;
using System;
using System.Threading.Tasks;
using M_API.Application.UseCases;
using M_API.Application.Services;

namespace M_API.API.Controllers
{
    [ApiController]
    [Route("api/sellers")]
    public class SellerSalesController : ControllerBase
    {
        private readonly GetSellerSalesUseCase _getSellerSales;
        private readonly IVendorProfileService _vendorService;

        public SellerSalesController(GetSellerSalesUseCase getSellerSales, IVendorProfileService vendorService)
        {
            _getSellerSales = getSellerSales;
            _vendorService = vendorService;
        }

        [HttpGet("sales")]
        public async Task<IActionResult> GetMySales()
        {
            try
            {
                var vendorProfileId = await _vendorService.GetVendorProfileIdAsync(User);
                var sales = await _getSellerSales.ExecuteAsync(vendorProfileId);
                return Ok(sales);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
