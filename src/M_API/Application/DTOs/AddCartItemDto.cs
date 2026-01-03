namespace Application.DTOs
{
    public record AddCartItemDto(
        Guid ProductId,
        int Quantity
    );
}

