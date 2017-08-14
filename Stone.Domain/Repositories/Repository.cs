using Stone.Common.Interfaces;
using Stone.Domain.Interfaces;
using System.Threading.Tasks;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Stone.Domain.Repositories
{
    public class Repository : IRepository
    {
        private IDataProvider _provider;

        public Repository(IDataProvider provider)
        {
            _provider = provider;
        }

        public async Task Create<T>(T entity) where T : class
        {
            await _provider.Create(entity);
        }

        public async Task CreateMany<T>(IEnumerable<T> entity) where T : class
        {
            await _provider.CreateMany(entity);
        }

        public async Task Delete<T>(Expression<Func<T, bool>> filter) where T : class
        {
            await _provider.Delete(filter);
        }

        public async Task<IEnumerable<T>> FindWhere<T>(Expression<Func<T, bool>> filter) where T : class
        {
            return await _provider.FindWhere(filter);
        }

        public async Task<IEnumerable<T>> GetAll<T>() where T : class
        {
            return await _provider.GetAll<T>();
        }

        public async Task<IEnumerable<T>> GetAll<T, TKey>(Func<T, TKey> orderByDescending, int page) where T : class
        {
            return await _provider.GetAll(orderByDescending, page);
        }
    }
}
