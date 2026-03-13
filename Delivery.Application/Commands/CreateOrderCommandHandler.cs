using Delivery.Domain.Entities;
using Delivery.Domain.Interfaces;
using MediatR;

namespace Delivery.Application.Commands;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly IOrderRepository _orderRepository;

    public CreateOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Order(request.CustomerId);

        await _orderRepository.AddAsync(order);

        return order.Id;
    }
}