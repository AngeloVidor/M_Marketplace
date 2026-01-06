using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stripe;

namespace M_API.Application.Services
{
    public class StripeConnectService : IStripeConnectService
    {
        public async Task<string> CreateConnectedAccountAsync(string email, Guid userId)
        {
            var options = new AccountCreateOptions
            {
                Type = "standard",
                Country = "BR",
                Email = email,
                Metadata = new Dictionary<string, string>
                {
                    { "user_id", userId.ToString() }
                }
            };

            var service = new AccountService();
            var account = await service.CreateAsync(options);

            return account.Id;
        }

        public async Task<string> CreateOnboardingLinkAsync(string stripeAccountId)
        {
            var options = new AccountLinkCreateOptions
            {
                Account = stripeAccountId,
                RefreshUrl = "http://localhost:5222/cancel",
                ReturnUrl = "http://localhost:5222/success",
                Type = "account_onboarding",

            };

            var service = new AccountLinkService();
            var link = await service.CreateAsync(options);

            return link.Url;
        }
    }
}