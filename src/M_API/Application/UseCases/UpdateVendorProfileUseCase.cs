using Domain.Entities;
using Domain.Repositories;
using Application.DTOs;
using Domain.ValueObjects;
using M_API.Domain.ValueObjects;

namespace Application.UseCases
{
    public class UpdateVendorProfileUseCase
    {
        private readonly IVendorProfileRepository _repository;

        public UpdateVendorProfileUseCase(IVendorProfileRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(Guid vendorId, CreateVendorProfileDto dto)
        {
            var vendor = await _repository.GetByIdAsync(vendorId);
            if (vendor == null)
                throw new Exception("Vendor profile not found.");

            var ownerName = new FullName(dto.FirstName, dto.LastName);
            var companyName = new CompanyName(dto.CompanyName);
            var cnpj = new Cnpj(dto.Cnpj);
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
            var phone = new Phone(dto.Phone);

            vendor.UpdateProfile(ownerName, companyName, cnpj, address, phone);

            await _repository.UpdateAsync(vendor);
            await _repository.SaveChangesAsync();
        }
    }
}
