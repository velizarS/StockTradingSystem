namespace OrderService.Messaging
{
    public class RabbitMqSettings
    {
        public string HostName { get; set; } = "localhost";
        public int Port { get; set; } = 5672;
        public string UserName { get; set; } = "guest";
        public string Password { get; set; } = "guest";
        public string Exchange { get; set; } = "orders-exchange";
        public string RoutingKey { get; set; } = "orders.created";
        public string Queue { get; set; } = "orders.created.queue";
    }
}
