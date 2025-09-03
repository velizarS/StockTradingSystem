using PortfolioService.Messaging;
using PortfolioService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortfolioService.Services
{
    public interface IPortfolioService
    {
        Task<IEnumerable<Portfolio>> GetAllAsync();
        Task<Portfolio?> GetByIdAsync(int id);
        Task AddAsync(Portfolio portfolio);
        Task UpdateAsync(Portfolio portfolio);
        Task DeleteAsync(int id);
        Task ProcessNewOrderAsync(OrderCreatedEvent orderEvent);
    }
}
