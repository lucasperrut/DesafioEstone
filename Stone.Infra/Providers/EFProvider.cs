using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Stone.Common.Interfaces;
using System.Data.Entity;

namespace Stone.Infra.Providers
{
    public class EFProvider : IDataProvider
    {
        private DbContext _context;

        public EFProvider(DbContext context)
        {
            _context = context;
        }

        public async Task Create<T>(T entity) where T : class
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task CreateMany<T>(IEnumerable<T> entities) where T : class
        {
            _context.Set<T>().AddRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task Delete<T>(Expression<Func<T, bool>> filter) where T : class
        {
            var entity = _context.Set<T>().Where(filter);
            _context.Set<T>().RemoveRange(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> FindWhere<T>(Expression<Func<T, bool>> filter) where T : class
        {
            return await _context.Set<T>().Where(filter).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAll<T>() where T : class
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAll<T, TKey>(Func<T, TKey> orderByDescending, int page) where T : class
        {
            return await Task.FromResult(_context.Set<T>().OrderByDescending(orderByDescending).Skip((page - 1) * 10).Take(10).ToList());
        }
    }
}