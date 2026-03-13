using Delivery.Domain.Events;
using MediatR;

namespace Delivery.Application.Events;

public class OrderCreatedEventHandler : INotificationHandler<OrderCreatedEvent>
{
    public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Pedido criado: {notification.Order.Id}");

        return Task.CompletedTask;
    }
}