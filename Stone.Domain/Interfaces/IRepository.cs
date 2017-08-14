using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Stone.Domain.Interfaces
{
    public interface IRepository
    {
        Task<IEnumerable<T>> GetAll<T>() where T : class;
        Task<IEnumerable<T>> FindWhere<T>(Expression<Func<T, bool>> filter) where T : class;
        Task Create<T>(T entity) where T : class;
        Task CreateMany<T>(IEnumerable<T> entity) where T : class;
        Task Delete<T>(Expression<Func<T, bool>> filter) where T : class;
        Task<IEnumerable<T>> GetAll<T, TKey>(Func<T, TKey> orderByDescending, int page) where T : class;
    }
}
