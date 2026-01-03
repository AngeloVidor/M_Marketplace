namespace Domain.Entities
{
    public class Order
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public decimal TotalAmount { get; private set; }
        public string Status { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public ICollection<OrderItem> Items { get; private set; } = new List<OrderItem>();

        protected Order() { }

        public Order(Guid userId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Status = "Pending";
            CreatedAt = DateTime.UtcNow;
        }

        public void CalculateTotal()
        {
            TotalAmount = Items.Sum(i => i.Subtotal);
        }
    }
}
