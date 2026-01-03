using Stripe.Checkout;
using Domain.Repositories;
using Domain.Enums;
using Application.Security;

public class CreateCheckoutSessionUseCase
{
    private readonly IOrderRepository _orderRepo;
    private readonly IProductStripeRepository _productStripeRepo;

    public CreateCheckoutSessionUseCase(
        IOrderRepository orderRepo,
        IProductStripeRepository productStripeRepo
       )
    {
        _orderRepo = orderRepo;
        _productStripeRepo = productStripeRepo;
    }

    public async Task<string> ExecuteAsync(Guid orderId)
    {
        var order = await _orderRepo.GetByIdAsync(orderId)
            ?? throw new Exception("Order not found");

        if (order.Status != OrderStatus.PendingPayment)
            throw new Exception("Order is not payable");

        var lineItems = new List<SessionLineItemOptions>();

        foreach (var item in order.Items)
        {
            var productStripe = await _productStripeRepo
                .GetByProductIdAsync(item.ProductId)
                ?? throw new Exception("Stripe product not found");

            lineItems.Add(new SessionLineItemOptions
            {
                Price = productStripe.StripePriceId,
                Quantity = item.Quantity
            });
        }

        var options = new SessionCreateOptions
        {
            Mode = "payment",
            LineItems = lineItems,
            SuccessUrl = "http://localhost:5222/success",
            CancelUrl = "http://localhost:5222/cancel",
            Metadata = new Dictionary<string, string>
            {
                { "orderId", order.Id.ToString() },
                { "userId", order.UserId.ToString() }

            }
        };

        var service = new SessionService();
        var session = await service.CreateAsync(options);

        return session.Url;
    }
}
