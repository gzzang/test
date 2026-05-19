using Microsoft.AspNetCore.Mvc;
using Test.Ddd.Application.Orders;

namespace Test.Ddd.Api.Controllers;

[ApiController]
[Route("api/orders")]
public sealed class OrdersController(IOrderApplicationService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<OrderDetailsDto>>> GetAll(CancellationToken cancellationToken)
    {
        var orders = await service.ListAsync(cancellationToken);
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDetailsDto>> GetById(string id, CancellationToken cancellationToken)
    {
        var order = await service.GetByIdAsync(id, cancellationToken);
        return order is null ? NotFound() : Ok(order);
    }

    [HttpPost]
    public async Task<ActionResult<OrderDetailsDto>> Create(CreateOrderRequest request, CancellationToken cancellationToken)
    {
        var order = await service.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
    }

    [HttpPost("{id}/submit")]
    public async Task<ActionResult<OrderDetailsDto>> Submit(string id, CancellationToken cancellationToken)
    {
        var order = await service.SubmitAsync(id, cancellationToken);
        return Ok(order);
    }
}
