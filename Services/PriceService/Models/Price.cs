namespace PriceService.Models
{
    public class Price
    {
        public int Id { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public decimal CurrentPrice { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
