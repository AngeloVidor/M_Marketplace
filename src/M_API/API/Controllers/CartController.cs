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
    private readonly UpdateItemQuantityUseCase _updateItem;
    private readonly RemoveItemFromCartUseCase _removeItem;
    private readonly GetCartUseCase _getCart;

    public CartController(
        AddItemToCartUseCase addItem,
        UpdateItemQuantityUseCase updateItem,
        RemoveItemFromCartUseCase removeItem,
        GetCartUseCase getCart)
    {
        _addItem = addItem;
        _updateItem = updateItem;
        _removeItem = removeItem;
        _getCart = getCart;
    }

    [HttpPost("items")]
    [Authorize(Roles = "Customer")]
    public async Task<IActionResult> AddItem([FromBody] AddCartItemDto dto)
    {
        var userId = ClaimsHelper.GetUserId(User);
        await _addItem.ExecuteAsync(userId, dto.ProductId, dto.Quantity);
        return NoContent();
    }

    [HttpGet]
    [Authorize(Roles = "Customer")]
    public async Task<IActionResult> GetCart()
    {
        var userId = ClaimsHelper.GetUserId(User);
        var cart = await _getCart.ExecuteAsync(userId);
        return Ok(cart);
    }

    [HttpPut("items/{productId}")]
    [Authorize(Roles = "Customer")]
    public async Task<IActionResult> UpdateItem(Guid productId, [FromBody] AddCartItemDto dto)
    {
        var userId = ClaimsHelper.GetUserId(User);
        await _updateItem.ExecuteAsync(userId, productId, dto.Quantity);
        return NoContent();
    }

    [HttpDelete("items/{productId}")]
    [Authorize(Roles = "Customer")]
    public async Task<IActionResult> RemoveItem(Guid productId)
    {
        var userId = ClaimsHelper.GetUserId(User);
        await _removeItem.ExecuteAsync(userId, productId);
        return NoContent();
    }

    [HttpDelete]
    [Authorize(Roles = "Customer")]
    public async Task<IActionResult> ClearCart()
    {
        var userId = ClaimsHelper.GetUserId(User);
        var cart = await _getCart.ExecuteAsync(userId);
        foreach (var item in cart.Items.ToList())
        {
            await _removeItem.ExecuteAsync(userId, item.ProductId);
        }
        return NoContent();
    }
}
