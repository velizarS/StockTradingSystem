using Microsoft.EntityFrameworkCore;
using PriceService.Data;
using PriceService.Models;

namespace PriceService.Services
{
    public class PriceService : IPriceService
    {
        private readonly PriceDbContext _context;

        public PriceService(PriceDbContext context)
        {
            _context = context;
        }

        public async Task<List<Price>> GetAllAsync() => await _context.Prices.ToListAsync();

        public async Task<Price?> GetByIdAsync(int id) =>
            await _context.Prices.FirstOrDefaultAsync(p => p.Id == id);

        public async Task AddAsync(Price price)
        {
            _context.Prices.Add(price);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Price price)
        {
            _context.Prices.Update(price);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var price = await _context.Prices.FindAsync(id);
            if (price != null)
            {
                _context.Prices.Remove(price);
                await _context.SaveChangesAsync();
            }
        }
    }
}
