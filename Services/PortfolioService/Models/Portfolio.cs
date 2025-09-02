namespace PortfolioService.Models
{
    public class Portfolio
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string Symbol { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal AveragePrice { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
<<<<<<< HEAD
}
=======
}
>>>>>>> 16e64bac1335152d90dae2ae354a9ba993cfd07d
