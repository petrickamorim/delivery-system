namespace Delivery.Domain.Exceptions;

public class OrderInvalidStateException : DomainException
{
    public OrderInvalidStateException(string message)
        : base(message)
    {
    }
}