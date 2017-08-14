using Stone.Application.Interfaces;
using Stone.Domain.Entities;
using Stone.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Linq;

namespace Stone.Application.Services
{
    public class CityApplicationService : ICityApplicationService
    {
        private IRepository _repository;

        public CityApplicationService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Create(string nameCity)
        {
            var city = new City
            {
                ID = Guid.NewGuid(),
                Name = nameCity
            };

            await _repository.Create(city);

            return city.ID;
        }

        public async Task Delete(Expression<Func<City, bool>> filter)
        {
            await _repository.Delete(filter);
        }

        public async Task<IEnumerable<City>> GetAll(int page)
        {
            return await _repository.GetAll<City, DateTime>(c => orderByDateTemperature(c), page);
        }

        private DateTime orderByDateTemperature(City city)
        {
            if (city.Temperatures.Any())
                return city.Temperatures.OrderByDescending(t => t.Date).FirstOrDefault().Date;

            return DateTime.Now;
        }
    }
}
