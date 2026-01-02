using Application.DTOs;
using Application.Security;
using Domain.Repositories;
using Domain.Exceptions;

namespace Application.UseCases
{
    public class LoginUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _tokenGenerator;

        public LoginUseCase(
            IUserRepository userRepository,
            IJwtTokenGenerator tokenGenerator)
        {
            _userRepository = userRepository;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<string> ExecuteAsync(LoginDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);

            if (user is null || !user.Password.Verify(dto.Password))
                throw new DomainException("Credenciais inválidas.");

            if (!user.IsActive)
                throw new DomainException("Usuário inativo.");

            var claims = new JwtUserClaimsDto
            {
                UserId = user.Id,
                Username = user.Username,
                Email = user.Email.Value,
                Role = user.Role.ToString()
            };

            return _tokenGenerator.Generate(claims);

        }
    }
}
