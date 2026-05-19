using Test.Ddd.Domain.Orders;

namespace Test.Ddd.Domain.Tests;

public class OrderTests
{
    [Fact]
    public void Create_Computes_Total_From_Items()
    {
        var order = Order.Create(
            [
                new OrderItem(Guid.NewGuid(), "Keyboard", 2, new Money(199, "CNY")),
                new OrderItem(Guid.NewGuid(), "Mouse", 1, new Money(99, "CNY"))
            ]);

        Assert.Equal(497, order.Total.Amount);
        Assert.Equal("CNY", order.Total.Currency);
        Assert.Equal(OrderStatus.Draft, order.Status);
    }

    [Fact]
    public void Submit_Changes_Status_And_Emits_Domain_Event()
    {
        var order = Order.Create([new OrderItem(Guid.NewGuid(), "Keyboard", 1, new Money(199, "CNY"))]);

        order.Submit();

        Assert.Equal(OrderStatus.Submitted, order.Status);
        Assert.Single(order.DomainEvents);
    }
}
