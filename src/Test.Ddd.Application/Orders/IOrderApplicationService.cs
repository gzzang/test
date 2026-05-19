namespace Test.Ddd.Application.Orders;

public interface IOrderApplicationService
{
    Task<OrderDetailsDto> CreateAsync(CreateOrderRequest request, CancellationToken cancellationToken = default);

    Task<OrderDetailsDto?> GetByIdAsync(string id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<OrderDetailsDto>> ListAsync(CancellationToken cancellationToken = default);

    Task<OrderDetailsDto> SubmitAsync(string id, CancellationToken cancellationToken = default);
}
