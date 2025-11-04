using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BookingSystem.Core.common;
using BookingSystem.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Infrastructure.presistence.Repo
{
    public class Reposatory<T> : IReposatory<T> where T :class, IEntity
    {
        
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Reposatory(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<int> AddAsync(T Entity)
        {
            await _dbSet.AddAsync(Entity);
            return Entity.Id;
        }

        public void DeleteAsync(T Entity)
        {
            _dbSet.Remove(Entity);
        }

        public async Task<PagedResult<T>> GetAllAsync(int pageNumber, int pageSize,Expression<Func<T, bool>> predicate = null)
        {
            var query = _dbSet.AsQueryable();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            var TotoalItems = await _dbSet.CountAsync();
            
            var items = await query
            .Skip((pageNumber - 1) * pageSize).
            Take(pageSize).ToListAsync();
            return new PagedResult<T>
            {
                Items = items,
                TotalItems = TotoalItems,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<T?> FindAsync(Expression<Func<T, bool>> predicate)
        => await _dbSet.FirstOrDefaultAsync(predicate);


        public async Task<bool> UpdateAsync(T Entity)
        {
            var existing = await _dbSet.FindAsync(Entity.Id);
            if (existing == null) return false;

            _context.Entry(existing).CurrentValues.SetValues(Entity);
            return true;
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}