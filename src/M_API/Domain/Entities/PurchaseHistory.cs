namespace Domain.Entities
{
    public class PurchaseHistory
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public DateTime PurchasedAt { get; private set; }

        private PurchaseHistory() { }

        public PurchaseHistory(Guid userId, Guid productId, string productName, int quantity, decimal unitPrice)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;
            PurchasedAt = DateTime.UtcNow;
        }
    }
}
