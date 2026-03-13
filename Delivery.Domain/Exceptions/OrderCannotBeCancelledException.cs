namespace Delivery.Domain.Exceptions;

public class OrderCannotBeCancelledException : DomainException
{
    public OrderCannotBeCancelledException()
        : base("Pedidos concluídos não podem ser cancelados.")
    {
    }
}