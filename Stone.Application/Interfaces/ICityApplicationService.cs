using Stone.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stone.Application.Interfaces
{
    public interface ICityApplicationService : IApplicationService<City>
    {
        Task<Guid> Create(string nameCity);
        Task<IEnumerable<City>> GetAll(int page);
    }
}
