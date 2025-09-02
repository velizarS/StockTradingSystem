using PortfolioService.Models;

namespace PortfolioService.Services
{
    public interface IPortfolioService
    {
        Task<List<Portfolio>> GetAllAsync();
        Task<Portfolio?> GetByIdAsync(int id);
        Task AddAsync(Portfolio portfolio);
        Task UpdateAsync(Portfolio portfolio);
        Task DeleteAsync(int id);
    }
}
