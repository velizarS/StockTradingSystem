using PriceService.Models;

namespace PriceService.Services
{
    public interface IPriceService
    {
        Task<List<Price>> GetAllAsync();
        Task<Price?> GetByIdAsync(int id);
        Task AddAsync(Price price);
        Task UpdateAsync(Price price);
        Task DeleteAsync(int id);
    }
}
