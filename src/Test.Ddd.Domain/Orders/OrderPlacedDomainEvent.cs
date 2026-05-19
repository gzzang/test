using Test.Ddd.Domain.Abstractions;

namespace Test.Ddd.Domain.Orders;

public sealed record OrderPlacedDomainEvent(OrderId OrderId, DateTimeOffset OccurredOn) : IDomainEvent;
