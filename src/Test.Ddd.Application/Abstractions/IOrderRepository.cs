using Test.Ddd.Domain.Orders;

namespace Test.Ddd.Application.Abstractions;

public interface IOrderRepository
{
    Task AddAsync(Order order, CancellationToken cancellationToken = default);

    Task<Order?> GetByIdAsync(OrderId id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Order>> ListAsync(CancellationToken cancellationToken = default);
}
