using PortfolioService.Models;
using PortfolioService.Repositories;
using PortfolioService.Messaging;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PortfolioService.Services
{
    public class PortfolioService : IPortfolioService
    {
        private readonly IRepository<Portfolio> _portfolioRepository;

        public PortfolioService(IRepository<Portfolio> portfolioRepository)
        {
            _portfolioRepository = portfolioRepository;
        }

        public async Task<IEnumerable<Portfolio>> GetAllAsync()
        {
            return await _portfolioRepository.GetAllAsync();
        }

        public async Task<Portfolio?> GetByIdAsync(int id)
        {
            return await _portfolioRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Portfolio portfolio)
        {
            await _portfolioRepository.AddAsync(portfolio);
            await _portfolioRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(Portfolio portfolio)
        {
            var existing = await _portfolioRepository.GetByIdAsync(portfolio.Id);
            if (existing == null)
                throw new KeyNotFoundException($"Portfolio with id {portfolio.Id} not found");

            existing.Symbol = portfolio.Symbol;
            existing.AveragePrice = portfolio.AveragePrice;
            existing.Quantity = portfolio.Quantity;

            await _portfolioRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var portfolio = await _portfolioRepository.GetByIdAsync(id);
            if (portfolio == null)
                throw new KeyNotFoundException($"Portfolio with id {id} not found");

            await _portfolioRepository.RemoveAsync(portfolio);
            await _portfolioRepository.SaveChangesAsync();
        }

        public async Task ProcessNewOrderAsync(OrderCreatedEvent orderEvent)
        {
            var portfolio = await _portfolioRepository.GetByIdAsync(orderEvent.OrderId);
            if (portfolio == null)
            {
                portfolio = new Portfolio
                {
                    Symbol = orderEvent.Symbol,
                    Quantity = orderEvent.Quantity,
                    AveragePrice = orderEvent.Price
                };
                await _portfolioRepository.AddAsync(portfolio);
            }
            else
            {
                // Актуализиране на количеството и средната цена
                var totalQuantity = portfolio.Quantity + orderEvent.Quantity;
                portfolio.AveragePrice = ((portfolio.AveragePrice * portfolio.Quantity) + (orderEvent.Price * orderEvent.Quantity)) / totalQuantity;
                portfolio.Quantity = totalQuantity;
            }

            await _portfolioRepository.SaveChangesAsync();
        }
    }
}
