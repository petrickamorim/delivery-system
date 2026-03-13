using MediatR;

namespace Delivery.Domain.Events;

public abstract class DomainEvent : INotification
{
    public DateTime OccurredOn { get; private set; }

    protected DomainEvent()
    {
        OccurredOn = DateTime.UtcNow;
    }
}