using Stone.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stone.Domain.Entities;
using Stone.Domain.Interfaces;
using System.Linq.Expressions;

namespace Stone.Application.Services
{
    public class TemperatureApplicationService : ITemperatureApplicationService
    {
        private IRepository _repository;

        public TemperatureApplicationService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task Delete(Expression<Func<Temperature, bool>> filter)
        {
            await _repository.Delete(filter);
        }

        public async Task<IEnumerable<Temperature>> Find(string cityName)
        {
            var dateComparer = DateTime.Now.AddHours(-30);
            var result = await _repository.FindWhere<Temperature>(c => c.City.Name == cityName && c.Date > dateComparer);


            return result.OrderBy(t => t.Date);
        }

        public async Task<IEnumerable<Temperature>> GetAll()
        {
            return await _repository.GetAll<Temperature>();
        }
    }
}
