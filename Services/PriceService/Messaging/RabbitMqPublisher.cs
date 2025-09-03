using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Shared.Events;

namespace PriceService.Messaging
{
    public class RabbitMqPublisher : IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _exchange;

        public RabbitMqPublisher(IOptions<RabbitMqSettings> options)
        {
            var settings = options.Value;
            _exchange = settings.Exchange;

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
        }

        public void PublishPriceUpdated(PriceUpdatedEvent evt)
        {
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(evt));

            var props = _channel.CreateBasicProperties();
            props.ContentType = "application/json";
            props.DeliveryMode = 2;

            _channel.BasicPublish(
                exchange: _exchange,
                routingKey: "price.updated",
                basicProperties: props,
                body: body
            );
        }

        public void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
        }
    }
}
