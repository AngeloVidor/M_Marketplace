using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Services;
using Domain.Entities;
using Domain.ValueObjects;
using M_API.Application.DTOs;
using M_API.Domain.Repositories;
using M_API.Domain.ValueObjects;

namespace M_API.Application.UseCases
{
    public class RegisterPendingUserUseCase
    {
        private readonly IPendingRegistrationRepository _pendingRepo;
        private readonly IEmailSender _emailSender;

        public RegisterPendingUserUseCase(IPendingRegistrationRepository pendingRepo, IEmailSender emailSender)
        {
            _pendingRepo = pendingRepo;
            _emailSender = emailSender;
        }

        public async Task ExecuteAsync(RegisterPendingUserDto dto)
        {
            var roleEnum = Enum.Parse<Role>(dto.Role, ignoreCase: true);

            var pending = new PendingRegistration(
                dto.Username,
                new Email(dto.Email),
                PasswordHash.Create(dto.Password),
                roleEnum
            );

            await _pendingRepo.AddAsync(pending);
            await _pendingRepo.SaveAsync();

            await _emailSender.SendAsync(new EmailMessage(
                pending.Email.Value,
                "Activate your account",
                $"Your activation code is: <b>{pending.ActivationToken.Token}</b>"
            ));
        }
    }
}
