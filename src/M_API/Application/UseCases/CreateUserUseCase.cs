using Application.DTOs;
using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;

namespace Application.UseCases
{
    public class CreateUserUseCase
    {
        private readonly IUserRepository _repository;

        public CreateUserUseCase(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(CreateUserDto dto)
        {
            var user = new User(
                dto.Username,
                new Email(dto.Email),
                PasswordHash.Create(dto.Password),
                dto.Role
            );

            await _repository.AddAsync(user);
        }
    }
}
