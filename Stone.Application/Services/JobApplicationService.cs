using Stone.Application.Interfaces;
using Stone.Application.Resources;
using Stone.Common.Interfaces;
using Stone.Domain.Entities;
using Stone.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;

namespace Stone.Application.Services
{
    public class JobApplicationService : IJobApplicationService
    {
        private IHttpAgent _user;
        private IRepository _repository;
        public JobApplicationService(IHttpAgent user, IRepository repository)
        {
            _user = user;
            _repository = repository;
        }

        public async Task CreateTemperature()
        {
            var citiesName = await GetCitiesName();
            var Temperatures = await RequestTemperatures(citiesName);

            if (Temperatures != null)
                await _repository.CreateMany(Temperatures);
        }

        private async Task<IEnumerable<City>> GetCitiesName()
        {
            return await _repository.GetAll<City>();
        }

        private async Task<IEnumerable<Temperature>> RequestTemperatures(IEnumerable<City> cities)
        {
            var Temperatures = new List<Temperature>();

            foreach (var item in cities)
            {
                var result = await (_user.GetAsync<ExpandoObject>(string.Format(StoneApplicationResources.APIWeather, item.Name))) as dynamic;
                var obj = ((result as IDictionary<string, object>)["results"] as IDictionary<string, object>);

                var temperature = new Temperature
                {
                    ID = Guid.NewGuid(),
                    CityID = item.ID,
                    Measure = obj["temp"].ToString(),
                    Date = DateTime.Parse($"{obj["date"].ToString()} {obj["time"].ToString()}")
                };

                Temperatures.Add(temperature);
            }

            return Temperatures;
        }
    }
}
