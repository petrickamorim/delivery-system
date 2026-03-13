using Delivery.Domain.Entities;

namespace Delivery.Domain.Events;

public class OrderCreatedEvent : DomainEvent
{
    public Order Order { get; }

    public OrderCreatedEvent(Order order)
    {
        Order = order;
    }
}