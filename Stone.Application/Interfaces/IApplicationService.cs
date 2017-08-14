using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Stone.Application.Interfaces
{
    public interface IApplicationService<T>
    {
        Task Delete(Expression<Func<T, bool>> filter);
    }
}
