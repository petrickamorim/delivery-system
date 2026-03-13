using Delivery.Domain.Entities;
using Delivery.Domain.Interfaces;

namespace Delivery.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly List<Order> _orders = new();

        public Task AddAsync(Order order)
        {
            _orders.Add(order);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Order>> GetAllAsync()
        {
            return Task.FromResult(_orders.AsEnumerable());
        }

        public Task<Order?> GetByIdAsync(Guid id)
        {
            var order = _orders.FirstOrDefault(x => x.Id == id);
            return Task.FromResult(order);
        }

        public Task UpdateAsync(Order order)
        {
            var existingOrder = _orders.FirstOrDefault(x => x.Id == order.Id);

            if (existingOrder != null)
            {
                _orders.Remove(existingOrder);
                _orders.Add(order);
            }

            return Task.CompletedTask;
        }
    }
}