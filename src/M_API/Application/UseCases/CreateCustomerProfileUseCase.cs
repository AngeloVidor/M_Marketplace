using Application.DTOs;
using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;

namespace Application.UseCases
{
    public class CreateCustomerProfileUseCase
    {
        private readonly ICustomerProfileRepository _repository;
        private readonly IUserRepository _userRepository;

        public CreateCustomerProfileUseCase(
            ICustomerProfileRepository repository,
            IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

        public async Task ExecuteAsync(CreateCustomerProfileDto dto)
        {
            var user = await _userRepository.GetByIdAsync(dto.UserId);
            if (user == null)
                throw new Exception("User not found.");

            var address = new Address(
                street: dto.Street,
                number: dto.Number,
                city: dto.City,
                state: dto.State,
                country: dto.Country,
                zipCode: dto.ZipCode,
                complement: dto.Complement,
                neighborhood: dto.Neighborhood
            );

            var profile = new CustomerProfile(
                dto.UserId,
                new FullName(dto.FirstName, dto.LastName),
                address,
                new Phone(dto.Phone)
            );

            await _repository.AddAsync(profile);
            await _repository.SaveChangesAsync();
        }
    }
}
