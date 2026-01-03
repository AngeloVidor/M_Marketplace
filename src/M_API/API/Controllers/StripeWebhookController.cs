using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using M_API.Application.UseCases;

namespace M_API.API.Controllers
{
    [ApiController]
    [Route("api/webhooks/stripe")]
    public class StripeWebhookController : ControllerBase
    {
        private readonly ConfirmOrderPaymentUseCase _useCase;

        public StripeWebhookController(ConfirmOrderPaymentUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpPost]
        public async Task<IActionResult> Handle()
        {
            var json = await new StreamReader(Request.Body).ReadToEndAsync();

            var stripeSignature = Request.Headers["Stripe-Signature"].FirstOrDefault();
            var webhookSecret = Environment.GetEnvironmentVariable("STRIPE_WEBHOOK_SECRET");

            if (string.IsNullOrEmpty(json) || string.IsNullOrEmpty(stripeSignature) || string.IsNullOrEmpty(webhookSecret))
            {
                return BadRequest("Missing webhook payload, signature, or secret");
            }

            Stripe.Event stripeEvent;

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
                if (session != null)
                {
                    await _useCase.ExecuteAsync(session);
                }
            }

            return Ok();
        }
    }
}
