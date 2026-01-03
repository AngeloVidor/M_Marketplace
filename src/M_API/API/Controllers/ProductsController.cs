using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.UseCases;
using Application.Security;

[ApiController]
[Route("api/products")]
[Authorize]
public class ProductsController : ControllerBase
{
    private readonly CreateProductUseCase _useCase;

    public ProductsController(CreateProductUseCase useCase)
    {
        _useCase = useCase;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Vendor")]
    public async Task<IActionResult> Create(CreateProductDto dto)
    {
        var userId = ClaimsHelper.GetUserId(User);

        await _useCase.ExecuteAsync(userId, dto);

        return Ok(new { message = "Product created successfully." });
    }
}
