using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using M_API.Domain.Entities;

namespace M_API.Domain.Repositories
{
    public interface ISellerProfileRepository
    {
        Task AddAsync(VendorProfile profile);

    }
}