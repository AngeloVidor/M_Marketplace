using M_API.Domain.ValueObjects;

namespace Application.DTOs
{
    public record CreateUserDto(
        string Username,
        string Email,
        string Password,
        Role Role
    );
}
