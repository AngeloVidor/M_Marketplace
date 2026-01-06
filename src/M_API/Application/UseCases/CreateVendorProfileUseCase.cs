using Application.DTOs;
using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;
using M_API.Application.Services;
using M_API.Domain.ValueObjects;

namespace Application.UseCases
{
    public class CreateVendorProfileUseCase
    {
        private readonly IVendorProfileRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IStripeConnectService _stripeService;

        public CreateVendorProfileUseCase(
            IVendorProfileRepository repository,
            IUserRepository userRepository,
            IStripeConnectService stripeService
        )
        {
            _repository = repository;
            _userRepository = userRepository;
            _stripeService = stripeService;
        }

        public async Task<string> ExecuteAsync(CreateVendorProfileDto dto)
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

            var stripeAccountId = await _stripeService.CreateConnectedAccountAsync(user.Email.Value, dto.UserId);
            vendor.AttachStripeAccount(stripeAccountId);
            await _repository.SaveChangesAsync();

            var onboardingUrl = await _stripeService.CreateOnboardingLinkAsync(stripeAccountId);

            return onboardingUrl;
        }

    }
}
