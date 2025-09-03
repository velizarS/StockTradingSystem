using OrderService.Messaging;
using OrderService.Models;
using OrderService.Repositories;

namespace OrderService.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IEventPublisher _publisher;

        public OrderService(IOrderRepository repository, IEventPublisher publisher)
        {
            _repository = repository;
            _publisher = publisher;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            order.CreatedAt = DateTime.UtcNow; // по-добре винаги UTC време

            await _repository.AddAsync(order);
            await _repository.SaveChangesAsync();

            // след успешен запис в базата -> публикуваме събитие
            var evt = new OrderCreatedEvent
            {
                OrderId = order.Id,
                Symbol = order.Symbol,
                Quantity = order.Quantity,
                Price = order.Price,
                CreatedAt = order.CreatedAt
            };

            _publisher.PublishOrderCreated(evt);

            return order;
        }
    }
}
