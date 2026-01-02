using Domain.Exceptions;

namespace Domain.ValueObjects
{
    public sealed class Money
    {
        public decimal Value { get; }

        public Money(decimal value)
        {
            if (value <= 0)
                throw new DomainException("Price must be greater than zero.");

            Value = value;
        }
    }
}
