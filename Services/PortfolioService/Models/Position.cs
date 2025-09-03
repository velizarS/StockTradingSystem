namespace PortfolioService.Models
{
    public class Position
    {
        public int Id { get; set; }

        public string Symbol { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public decimal PurchasePrice { get; set; }

        public decimal CurrentPrice { get; set; }
    }
}
