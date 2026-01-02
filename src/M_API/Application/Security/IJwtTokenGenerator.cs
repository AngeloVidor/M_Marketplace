namespace Application.Security
{
    public interface IJwtTokenGenerator
    {
        string Generate(JwtUserClaimsDto claims);
    }
}
