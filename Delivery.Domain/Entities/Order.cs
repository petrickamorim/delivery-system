using Delivery.Domain.Enums;
using Delivery.Domain.Exceptions;
using Delivery.Domain.Events;

namespace Delivery.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; private set; }
        public Guid CustomerId { get; private set; }
        public OrderStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private readonly List<OrderItem> _items = new();
        private readonly List<DomainEvent> _domainEvents = new();
        public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();
        public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        private Order() { }

        private void AddDomainEvent(DomainEvent eventItem)
        {
            _domainEvents.Add(eventItem);
        }

        public Order(Guid customerId)
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
            Status = OrderStatus.Created;
            CreatedAt = DateTime.UtcNow;

            AddDomainEvent(new OrderCreatedEvent(this));
        }

        public void AddItem(Guid productId, int quantity, decimal price)
        {
            if (Status != OrderStatus.Created)
                throw new OrderInvalidStateException("Itens só podem ser adicionados em pedidos recém-criados.");

            var item = new OrderItem(productId, quantity, price);

            _items.Add(item);
        }

        public void RemoveItem(Guid itemId)
        {
            var item = _items.FirstOrDefault(x => x.Id == itemId);

            if (item == null)
                throw new OrderInvalidStateException("Item não encontrado neste pedido.");

            if(item == null)
                throw new OrderInvalidStateException("Item não encontrado neste pedido.");

            _items.Remove(item);
        }

        public decimal GetTotal()
        {
            return _items.Sum(x => x.GetTotal());
        }

        public void MarkAsCompleted()
        {
            if(Status != OrderStatus.InDelivery)
                throw new OrderInvalidStateException("Apenas pedidos que estão em entrega podem ser marcados como concluídos.");

            Status = OrderStatus.Completed;

            AddDomainEvent(new OrderCompletedEvent(this));
        }

        public void MarkAsInDelivery()
        {
            if(Status != OrderStatus.Created)
                throw new OrderInvalidStateException("Apenas pedidos recém-criados podem ser enviados para entrega.");

            if(!_items.Any())
                throw new OrderInvalidStateException("Não é possível enviar um pedido sem itens para entrega.");

            Status = OrderStatus.InDelivery;
        }

        public void MarkAsCancelled()
        {
            if (Status == OrderStatus.Completed)
                throw new OrderCannotBeCancelledException();

            if(Status == OrderStatus.Cancelled)
                throw new OrderInvalidStateException("Este pedido já está cancelado.");

            Status = OrderStatus.Cancelled;
        }
    }
}