using Domain.Exceptions;

namespace Domain.ValueObjects
{
    public sealed class Stock
    {
        public int Quantity { get; }

        public Stock(int quantity)
        {
            if (quantity < 0)
                throw new DomainException("Stock cannot be negative.");

            Quantity = quantity;
        }

        public Stock Add(int amount)
        {
            if (amount <= 0)
                throw new DomainException("Invalid quantity.");

            return new Stock(Quantity + amount);
        }

        public Stock Remove(int amount)
        {
            if (amount <= 0)
                throw new DomainException("Invalid quantity.");

            if (Quantity - amount < 0)
                throw new DomainException("Insufficient stock.");

            return new Stock(Quantity - amount);
        }
    }
}
