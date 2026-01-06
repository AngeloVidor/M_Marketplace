using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Repositories;

namespace M_API.Application.UseCases
{
    public class DeleteVendorProfileUseCase
    {
        private readonly IVendorProfileRepository _repository;

        public DeleteVendorProfileUseCase(IVendorProfileRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(Guid id)
        {
            var vendor = await _repository.GetByIdAsync(id);
            if (vendor == null)
                throw new Exception("Vendor not found.");

            await _repository.DeleteAsync(vendor);
            await _repository.SaveChangesAsync();
        }
    }

}