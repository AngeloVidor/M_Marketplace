using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;

namespace M_API.Application.UseCases
{
    public class GetAllVendorProfilesUseCase
    {
        private readonly IVendorProfileRepository _repository;

        public GetAllVendorProfilesUseCase(IVendorProfileRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<VendorProfile>> ExecuteAsync()
        {
            return await _repository.GetAllAsync();
        }
    }

}