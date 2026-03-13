using MediatR;

namespace Delivery.Application.Commands
{
    public class CreateOrderCommand : IRequest<Guid>
    {
        public Guid CustomerId { get; set; }
    }
}
