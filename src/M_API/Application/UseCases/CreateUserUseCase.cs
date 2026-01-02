// using Application.DTOs;
// using Application.Services;
// using Domain.Entities;
// using Domain.Repositories;
// using Domain.ValueObjects;
// using M_API.Application.DTOs;
// using M_API.Domain.Repositories;

// namespace Application.UseCases
// {
//     public class CreateUserUseCase
//     {
//         private readonly IUserRepository _repository;
//         private readonly IEmailSender _emailSender;

//         public CreateUserUseCase(IUserRepository repository, IEmailSender emailSender)
//         {
//             _repository = repository;
//             _emailSender = emailSender;
//         }

//         public async Task ExecuteAsync(CreateUserDto dto)
//         {
//             var user = new User(
//                 dto.Username,
//                 new Email(dto.Email),
//                 PasswordHash.Create(dto.Password),
//                 dto.Role
//             );

//             await _repository.AddAsync(user);
//             var token = new ActivationToken(user.Id);

//             //await _tokens.AddAsync(token);

//             await _repository.SaveAsync();

//             await _emailSender.SendAsync(new EmailMessage(
//                 user.Email.Value,
//                 "Activate your account",
//                 $"Your activation code is: <b>{token.Token}</b>"
//             ));
//         }
//     }
// }
