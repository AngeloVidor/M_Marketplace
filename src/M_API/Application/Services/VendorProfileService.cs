using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Security;
using Domain.Repositories;

namespace M_API.Application.Services
{
    public class VendorProfileService : IVendorProfileService
    {
        private readonly IVendorProfileRepository _vendorRepo;

        public VendorProfileService(IVendorProfileRepository vendorRepo)
        {
            _vendorRepo = vendorRepo;
        }

        public async Task<Guid> GetVendorProfileIdAsync(ClaimsPrincipal user)
        {
            var userId = ClaimsHelper.GetUserId(user);

            var vendorProfile = await _vendorRepo.GetByUserIdAsync(userId)
                ?? throw new Exception("Vendor profile not found for this user.");

            return vendorProfile.Id;
        }
    }
}
