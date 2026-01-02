using Domain.Entities;
using Domain.Repositories;
using Application.DTOs;
using M_API.Domain.Repositories;

namespace Application.UseCases
{
    public class ActivateUserUseCase
    {
        private readonly IPendingRegistrationRepository _pendingRepo;
        private readonly IUserRepository _userRepo;

        public ActivateUserUseCase(IPendingRegistrationRepository pendingRepo, IUserRepository userRepo)
        {
            _pendingRepo = pendingRepo;
            _userRepo = userRepo;
        }

        public async Task ExecuteAsync(ActivateUserDto dto)
        {
            var pending = await _pendingRepo.GetByEmailAsync(dto.Email);
            if (pending is null)
                throw new Exception("Pending registration not found");

            if (pending.ActivationToken.Token != dto.Token)
                throw new Exception("Invalid token");

            if (pending.ActivationToken.IsExpired())
                throw new Exception("Token expired");

            var user = new User(
                pending.Username,
                pending.Email,
                pending.Password,
                pending.Role
            );

            await _userRepo.AddAsync(user);
            await _userRepo.SaveAsync();

            await _pendingRepo.RemoveAsync(pending);
            await _pendingRepo.SaveAsync();
        }
    }
}
