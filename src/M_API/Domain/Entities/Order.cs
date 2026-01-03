using Domain.Entities;
using Domain.Enums;

public class Order
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public decimal Total { get; private set; }
    public OrderStatus Status { get; private set; }
    public List<OrderItem> Items { get; private set; } = new();

    protected Order() { }

    public Order(Guid userId)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Status = OrderStatus.PendingPayment;
    }

    public void CalculateTotal()
    {
        Total = Items.Sum(i => i.Subtotal);
    }

    public void MarkAsPaid()
    {
        Status = OrderStatus.Paid;
    }
}
