using Domain.ValueObjects;
using Domain.Exceptions;

namespace Domain.Entities
{
    public class Product
    {
        public Guid Id { get; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Money Price { get; private set; }
        public Stock Stock { get; private set; }
        public Guid OwnerId { get; }
        public DateTime CreatedAt { get; }

        public bool IsAvailable => Stock.Quantity > 0;

        public Product(string name, string description, Money price, Stock stock, Guid ownerId)
        {
            Id = Guid.NewGuid();

            Name = name ?? throw new DomainException("Invalid name.");
            Description = description ?? string.Empty;
            Price = price ?? throw new DomainException("Invalid price.");
            Stock = stock ?? throw new DomainException("Invalid stock.");
            OwnerId = ownerId;
            CreatedAt = DateTime.UtcNow;
        }

        public void ChangePrice(Money newPrice)
        {
            Price = newPrice ?? throw new DomainException("Invalid price.");
        }

        public void AddStock(int amount)
        {
            Stock = Stock.Add(amount);
        }

        public void RemoveStock(int amount)
        {
            Stock = Stock.Remove(amount);
        }
    }
}
