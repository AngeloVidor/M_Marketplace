namespace Application.Security
{
    public class JwtUserClaimsDto
    {
        public Guid UserId { get; init; }
        public string Username { get; init; } = default!;
        public string Email { get; init; } = default!;
        public string Role { get; init; } = default!;
    }
}
