namespace PortfolioService.Messaging
{
    public class OrderCreatedEvent
    {
        public int OrderId { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
