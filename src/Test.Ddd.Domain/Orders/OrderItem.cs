namespace Test.Ddd.Domain.Orders;

public sealed record OrderItem(Guid ProductId, string ProductName, int Quantity, Money UnitPrice)
{
    public decimal LineTotal => Quantity * UnitPrice.Amount;
}
