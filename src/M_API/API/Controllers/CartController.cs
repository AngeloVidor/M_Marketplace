using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Security;
using M_API.Application.UseCases;
using Application.DTOs;

[ApiController]
[Route("api/cart")]
[Authorize]
public class CartController : ControllerBase
{
    private readonly AddItemToCartUseCase _addItem;


    public CartController(
        AddItemToCartUseCase addItem)
    {
        _addItem = addItem;
    }

    [HttpPost("items")]
    public async Task<IActionResult> AddItem([FromBody] AddCartItemDto dto)
    {
        var userId = ClaimsHelper.GetUserId(User);
        await _addItem.ExecuteAsync(userId, dto.ProductId, dto.Quantity);
        return NoContent();
    }
}
