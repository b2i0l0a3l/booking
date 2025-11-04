using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BookingSystem.Core.common;

namespace BookingSystem.Core.Interfaces
{
    public interface IReposatory<T> where T : class
    {
        Task SaveAsync();
        Task<PagedResult<T>> GetAllAsync(int pageNumber, int pageSize,Expression<Func<T, bool>> predicate = null);
        Task<T?> FindAsync(Expression<Func<T, bool>> predicate);
        Task<int> AddAsync(T Entity);
        void DeleteAsync(T Entity);
        Task<bool> UpdateAsync(T Entity);
    }
}