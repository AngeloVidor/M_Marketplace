using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace M_API.Application.Services
{
    public interface IVendorProfileService
    {
        Task<Guid> GetVendorProfileIdAsync(ClaimsPrincipal user);
    }
}