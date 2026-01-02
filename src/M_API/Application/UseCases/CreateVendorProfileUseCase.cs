using Application.DTOs;
using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;
using M_API.Domain.ValueObjects;

namespace Application.UseCases
{
    public class CreateVendorProfileUseCase
    {
        private readonly IVendorProfileRepository _repository;
        private readonly IUserRepository _userRepository;

        public CreateVendorProfileUseCase(IVendorProfileRepository repository, IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

        public async Task ExecuteAsync(CreateVendorProfileDto dto)
        {
            var user = await _userRepository.GetByIdAsync(dto.UserId);
            if (user == null)
                throw new Exception("User not found.");

            var address = new Address(
                dto.Street,
                dto.Number,
                dto.City,
                dto.State,
                dto.Country,
                dto.ZipCode,
                dto.Complement,
                dto.Neighborhood
            );

            var vendor = new VendorProfile(
                dto.UserId,
                new FullName(dto.FirstName, dto.LastName),
                new CompanyName(dto.CompanyName),
                new Cnpj(dto.Cnpj),
                address,
                new Phone(dto.Phone)
            );

            await _repository.AddAsync(vendor);
            await _repository.SaveChangesAsync();
        }
    }
}
