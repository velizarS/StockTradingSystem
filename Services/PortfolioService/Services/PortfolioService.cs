using Microsoft.EntityFrameworkCore;
using PortfolioService.Data;
using PortfolioService.Models;

namespace PortfolioService.Services
{
    public class PortfolioService : IPortfolioService
    {
        private readonly PortfolioDbContext _context;

        public PortfolioService(PortfolioDbContext context)
        {
            _context = context;
        }

        public async Task<List<Portfolio>> GetAllAsync() => await _context.Portfolios.ToListAsync();

        public async Task<Portfolio?> GetByIdAsync(int id) =>
            await _context.Portfolios.FirstOrDefaultAsync(p => p.Id == id);

        public async Task AddAsync(Portfolio portfolio)
        {
            _context.Portfolios.Add(portfolio);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Portfolio portfolio)
        {
            _context.Portfolios.Update(portfolio);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var portfolio = await _context.Portfolios.FindAsync(id);
            if (portfolio != null)
            {
                _context.Portfolios.Remove(portfolio);
                await _context.SaveChangesAsync();
            }
        }
    }
}
