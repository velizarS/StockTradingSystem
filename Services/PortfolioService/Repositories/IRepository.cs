﻿using System.Linq.Expressions;

namespace PortfolioService.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        Task RemoveAsync(T entity);
        Task SaveChangesAsync();
    }
}
