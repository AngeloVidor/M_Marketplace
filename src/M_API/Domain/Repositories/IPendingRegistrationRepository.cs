using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace M_API.Domain.Repositories
{
    public interface IPendingRegistrationRepository
    {
        Task AddAsync(PendingRegistration pending);
        Task<PendingRegistration?> GetByEmailAsync(string email);
        Task<PendingRegistration?> GetByTokenAsync(string token);
        Task RemoveAsync(PendingRegistration pending);
        Task SaveAsync();
    }

}