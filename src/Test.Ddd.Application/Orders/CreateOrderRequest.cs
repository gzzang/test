namespace Test.Ddd.Application.Orders;

public sealed record CreateOrderRequest(IReadOnlyCollection<CreateOrderItemRequest> Items);

public sealed record CreateOrderItemRequest(Guid ProductId, string ProductName, int Quantity, decimal UnitPrice, string Currency);
