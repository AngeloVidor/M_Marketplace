using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.UseCases;
using Application.Security;
using M_API.Application.UseCases;

[ApiController]
[Route("api/products")]
[Authorize]
public class ProductsController : ControllerBase
{
    private readonly CreateProductUseCase _useCase;
    private readonly GetProductsUseCase _getUseCase;
    private readonly GetProductByIdUseCase _getByIdUseCase;
    private readonly UpdateProductUseCase _updateUseCase;
    private readonly DeleteProductUseCase _deleteUseCase;

    public ProductsController(CreateProductUseCase useCase, GetProductsUseCase getUseCase, GetProductByIdUseCase getByIdUseCase, UpdateProductUseCase updateUseCase, DeleteProductUseCase deleteUseCase)
    {
        _useCase = useCase;
        _getUseCase = getUseCase;
        _getByIdUseCase = getByIdUseCase;
        _updateUseCase = updateUseCase;
        _deleteUseCase = deleteUseCase;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Vendor")]
    public async Task<IActionResult> Create(CreateProductDto dto)
    {
        var userId = ClaimsHelper.GetUserId(User);

        await _useCase.ExecuteAsync(userId, dto);

        return Ok(new { message = "Product created successfully." });
    }

    [HttpGet]
    [Authorize(Roles = "Vendor")]
    public async Task<IActionResult> GetAll()
    {
        var userId = ClaimsHelper.GetUserId(User);
        var products = await _getUseCase.ExecuteAsync(userId);
        return Ok(products);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Vendor")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var userId = ClaimsHelper.GetUserId(User);
        var product = await _getByIdUseCase.ExecuteAsync(userId, id);
        return Ok(product);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Vendor")]
    public async Task<IActionResult> Update(Guid id, CreateProductDto dto)
    {
        var userId = ClaimsHelper.GetUserId(User);
        await _updateUseCase.ExecuteAsync(userId, id, dto);
        return Ok(new { message = "Product updated successfully." });
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Vendor")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var userId = ClaimsHelper.GetUserId(User);
        await _deleteUseCase.ExecuteAsync(userId, id);
        return Ok(new { message = "Product deleted successfully." });
    }
}
