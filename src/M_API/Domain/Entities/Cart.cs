namespace Domain.Entities
{
    public class Cart
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public bool IsConverted { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public ICollection<CartItem> Items { get; private set; } = new List<CartItem>();

        protected Cart() { }

        public Cart(Guid userId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            CreatedAt = DateTime.UtcNow;
            IsConverted = false;
        }

        public void Convert() => IsConverted = true;
    }
}
