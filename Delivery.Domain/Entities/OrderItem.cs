
using Delivery.Domain.Enums;

namespace Delivery.Domain.Entities
{
    public class OrderItem
    {
        public Guid Id { get; private set; }
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }

        private OrderItem() { }

        public OrderItem(Guid productId, int quantity, decimal price)
        {
           if(quantity <= 0)
                throw new ArgumentException("A quantidade deve ser maior que zero.");

           if (price <= 0)
                throw new ArgumentException("O preço deve ser maior que zero.");

            Id = Guid.NewGuid();
            ProductId = productId;
            Quantity = quantity;
            Price = price;
        }

        public decimal GetTotal()
        {
            return Quantity * Price;
        }
    }
}
