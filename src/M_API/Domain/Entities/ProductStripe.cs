namespace Domain.Entities
{
    public class ProductStripe
    {
        public Guid Id { get; private set; }
        public Guid ProductId { get; private set; }
        public string StripeProductId { get; private set; }
        public string StripePriceId { get; private set; }
        public DateTime CreatedAt { get; private set; }

        protected ProductStripe() { }

        public ProductStripe(Guid productId, string stripeProductId, string stripePriceId)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
            StripeProductId = stripeProductId;
            StripePriceId = stripePriceId;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
