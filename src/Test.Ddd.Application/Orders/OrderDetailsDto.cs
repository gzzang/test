namespace Test.Ddd.Application.Orders;

public sealed record OrderDetailsDto(
    string Id,
    DateTimeOffset CreatedAt,
    string Status,
    IReadOnlyCollection<OrderItemDto> Items,
    decimal TotalAmount,
    string TotalCurrency);

public sealed record OrderItemDto(Guid ProductId, string ProductName, int Quantity, decimal UnitPrice, string Currency);
