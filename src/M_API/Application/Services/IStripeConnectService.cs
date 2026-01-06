using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace M_API.Application.Services
{
    public interface IStripeConnectService
    {
        Task<string> CreateConnectedAccountAsync(string email, Guid userId);
        Task<string> CreateOnboardingLinkAsync(string stripeAccountId);
    }

}