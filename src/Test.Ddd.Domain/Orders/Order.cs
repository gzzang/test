using Test.Ddd.Domain.Abstractions;

namespace Test.Ddd.Domain.Orders;

public sealed class Order : AggregateRoot<OrderId>
{
    private readonly List<OrderItem> _items = [];

    private Order(OrderId id, IReadOnlyCollection<OrderItem> items, DateTimeOffset createdAt) : base(id)
    {
        _items.AddRange(items);
        CreatedAt = createdAt;
        Status = OrderStatus.Draft;
    }

    private Order()
    {
    }

    public DateTimeOffset CreatedAt { get; private set; }

    public OrderStatus Status { get; private set; }

    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    public Money Total => _items.Count == 0
        ? Money.Zero("CNY")
        : _items.Select(item => new Money(item.LineTotal, item.UnitPrice.Currency))
            .Aggregate((left, right) => left + right);

    public static Order Create(IEnumerable<OrderItem> items, DateTimeOffset? createdAt = null)
    {
        var itemList = items?.ToArray() ?? throw new ArgumentNullException(nameof(items));
        if (itemList.Length == 0)
        {
            throw new ArgumentException("An order must contain at least one item.", nameof(items));
        }

        var currency = itemList[0].UnitPrice.Currency;
        if (itemList.Any(item => !string.Equals(item.UnitPrice.Currency, currency, StringComparison.OrdinalIgnoreCase)))
        {
            throw new InvalidOperationException("All order items must use the same currency.");
        }

        return new Order(OrderId.New(), itemList, createdAt ?? DateTimeOffset.UtcNow);
    }

    public void Submit()
    {
        if (Status != OrderStatus.Draft)
        {
            throw new InvalidOperationException("Only draft orders can be submitted.");
        }

        Status = OrderStatus.Submitted;
        AddDomainEvent(new OrderPlacedDomainEvent(Id, DateTimeOffset.UtcNow));
    }
}
