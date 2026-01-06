using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using Domain.Repositories;
using M_API.Application.UseCases;
using Microsoft.AspNetCore.Authorization;

namespace M_API.API.Controllers
{
    [ApiController]
    [Route("api/webhooks/stripe")]
    public class StripeWebhookController : ControllerBase
    {
        private readonly ConfirmOrderPaymentUseCase _paymentUseCase;
        private readonly IVendorProfileRepository _vendorRepository;
        private readonly IConfiguration _configuration;

        public StripeWebhookController(
            ConfirmOrderPaymentUseCase paymentUseCase,
            IVendorProfileRepository vendorRepository,
            IConfiguration configuration)
        {
            _paymentUseCase = paymentUseCase;
            _vendorRepository = vendorRepository;
            _configuration = configuration;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Handle()
        {
            var json = await new StreamReader(Request.Body).ReadToEndAsync();
            var stripeSignature = Request.Headers["Stripe-Signature"].FirstOrDefault();
            var webhookSecret = Environment.GetEnvironmentVariable("STRIPE_WEBHOOK_SECRET");

            if (string.IsNullOrEmpty(json) || string.IsNullOrEmpty(stripeSignature) || string.IsNullOrEmpty(webhookSecret))
            {
                return BadRequest("Missing webhook payload, signature, or secret");
            }

            Event stripeEvent;

            try
            {
                stripeEvent = EventUtility.ConstructEvent(
                    json,
                    stripeSignature,
                    webhookSecret,
                    throwOnApiVersionMismatch: false
                );
            }
            catch (StripeException ex)
            {
                return BadRequest($"Webhook error: {ex.Message}");
            }

            if (stripeEvent.Type == "checkout.session.completed")
            {
                var session = stripeEvent.Data.Object as Session;
                if (session != null && session.PaymentStatus == "paid")
                {
                    await _paymentUseCase.ExecuteAsync(session);
                }
            }

            if (stripeEvent.Type == "account.updated")
            {
                Console.WriteLine(stripeEvent.Type);

                var account = stripeEvent.Data.Object as Account;
                if (account != null)
                {
                    //string id = "acct_1SmgTcQfNWWPKIWH";
                    var vendor = await _vendorRepository.GetByStripeAccountIdAsync(account.Id);
                    if (vendor != null)
                    {
                        var isActive = account.ChargesEnabled == true && account.PayoutsEnabled == true;
                        if (isActive)
                            vendor.ActivateStripe();

                        else
                            vendor.AttachStripeAccount(vendor.StripeAccountId!);


                        await _vendorRepository.SaveChangesAsync();

                    }
                    else
                    {
                        Console.WriteLine("Vendor not found for Stripe Account ID: " + account.Id);
                    }
                }
            }

            return Ok();
        }
    }
}
