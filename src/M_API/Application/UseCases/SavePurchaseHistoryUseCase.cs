using Domain.Entities;
using Domain.Repositories;
using Stripe.Checkout;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class SavePurchaseHistoryUseCase
    {
        private readonly IPurchaseHistoryRepository _historyRepo;

        public SavePurchaseHistoryUseCase(IPurchaseHistoryRepository historyRepo)
        {
            _historyRepo = historyRepo;
        }

        public async Task ExecuteAsync(Session session)
        {
            var userIdStr = session.Metadata["userId"];
            if (!Guid.TryParse(userIdStr, out var userId))
                throw new Exception("Invalid userId in metadata");

            var lineItemService = new SessionLineItemService();
            var lineItems = lineItemService.List(session.Id);

            foreach (var item in lineItems.Data)
            {
                var quantity = (int)(item.Quantity ?? 1);
                var productId = item.Price.ProductId;
                var amount = item.AmountTotal / 100m;

                var purchase = new PurchaseHistory(
                    userId,
                    Guid.Parse(productId),
                    item.Description,
                    quantity,
                    amount
                );

                await _historyRepo.AddAsync(purchase);
            }

            await _historyRepo.SaveChangesAsync();
        }
    }
}
