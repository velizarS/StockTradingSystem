using System.Text;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared.Events;
using PortfolioService.Data;

namespace PortfolioService.Messaging
{
    public class PriceUpdatedConsumer : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly string _exchange = "prices-exchange";

        public PriceUpdatedConsumer(IOptions<RabbitMqSettings> options, IServiceScopeFactory scopeFactory)
        {
            var settings = options.Value;
            _scopeFactory = scopeFactory;

            var factory = new ConnectionFactory
            {
                HostName = settings.HostName,
                Port = settings.Port,
                UserName = settings.UserName,
                Password = settings.Password
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(
                exchange: _exchange,
                type: ExchangeType.Direct,
                durable: true
            );

            _channel.QueueDeclare(
                queue: "portfolio-price-updates",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            _channel.QueueBind(
                queue: "portfolio-price-updates",
                exchange: _exchange,
                routingKey: "price.updated"
            );
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);

                var priceEvent = JsonSerializer.Deserialize<PriceUpdatedEvent>(json);

                if (priceEvent != null)
                {
                    using var scope = _scopeFactory.CreateScope();
                    var db = scope.ServiceProvider.GetRequiredService<PortfolioDbContext>();

                    var positions = db.Positions
                        .Where(p => p.Symbol == priceEvent.Symbol)
                        .ToList();

                    foreach (var pos in positions)
                    {
                        pos.CurrentPrice = priceEvent.NewPrice;
                    }

                    await db.SaveChangesAsync();
                }
            };

            _channel.BasicConsume(
                queue: "portfolio-price-updates",
                autoAck: true,
                consumer: consumer
            );

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
            base.Dispose();
        }
    }
}
