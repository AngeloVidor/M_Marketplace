namespace Domain.Entities
{
    public class OrderItem
    {
        public Guid Id { get; private set; }
        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
        public decimal UnitPrice { get; private set; }
        public int Quantity { get; private set; }

        public decimal Subtotal => UnitPrice * Quantity;

        protected OrderItem() { }

        public OrderItem(Guid orderId, Guid productId, string name, decimal price, int quantity)
        {
            Id = Guid.NewGuid();
            OrderId = orderId;
            ProductId = productId;
            ProductName = name;
            UnitPrice = price;
            Quantity = quantity;
        }
    }
}
