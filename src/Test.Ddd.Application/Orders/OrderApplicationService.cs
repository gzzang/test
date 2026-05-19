using Test.Ddd.Application.Abstractions;
using Test.Ddd.Domain.Orders;

namespace Test.Ddd.Application.Orders;

public sealed class OrderApplicationService(IOrderRepository repository) : IOrderApplicationService
{
    public async Task<OrderDetailsDto> CreateAsync(CreateOrderRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var order = Order.Create(
            request.Items.Select(item => new OrderItem(
                item.ProductId,
                item.ProductName,
                item.Quantity,
                new Money(item.UnitPrice, item.Currency))),
            DateTimeOffset.UtcNow);

        await repository.AddAsync(order, cancellationToken);
        return ToDto(order);
    }

    public async Task<OrderDetailsDto?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var order = await repository.GetByIdAsync(ParseOrderId(id), cancellationToken);
        return order is null ? null : ToDto(order);
    }

    public async Task<IReadOnlyList<OrderDetailsDto>> ListAsync(CancellationToken cancellationToken = default)
    {
        var orders = await repository.ListAsync(cancellationToken);
        return orders.Select(ToDto).ToArray();
    }

    public async Task<OrderDetailsDto> SubmitAsync(string id, CancellationToken cancellationToken = default)
    {
        var orderId = ParseOrderId(id);
        var order = await repository.GetByIdAsync(orderId, cancellationToken)
            ?? throw new KeyNotFoundException($"Order '{id}' was not found.");

        order.Submit();
        await repository.AddAsync(order, cancellationToken);
        return ToDto(order);
    }

    private static OrderId ParseOrderId(string id)
    {
        if (!Guid.TryParse(id, out var value))
        {
            throw new ArgumentException("Order id must be a valid GUID.", nameof(id));
        }

        return new OrderId(value);
    }

    private static OrderDetailsDto ToDto(Order order) =>
        new(
            order.Id.ToString(),
            order.CreatedAt,
            order.Status.ToString(),
            order.Items.Select(item => new OrderItemDto(
                item.ProductId,
                item.ProductName,
                item.Quantity,
                item.UnitPrice.Amount,
                item.UnitPrice.Currency)).ToArray(),
            order.Total.Amount,
            order.Total.Currency);
}
