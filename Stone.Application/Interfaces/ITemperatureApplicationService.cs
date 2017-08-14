using Stone.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stone.Application.Interfaces
{
    public interface ITemperatureApplicationService : IApplicationService<Temperature>
    {
        Task<IEnumerable<Temperature>> Find(string cityName);
    }
}
