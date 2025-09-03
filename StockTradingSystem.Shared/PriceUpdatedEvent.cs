namespace Shared.Events
{
    public class PriceUpdatedEvent
    {
        public string Symbol { get; set; } = string.Empty;
        public decimal NewPrice { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
