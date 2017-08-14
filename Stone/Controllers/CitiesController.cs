using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stone.Application.Interfaces;
using Stone.Models;
using Stone.Common.Interfaces;
using Stone.Application.Resources;

namespace Stone.Controllers
{
    [Produces("application/json")]
    [Route("api/cities")]
    public class CitiesController : Controller
    {
        private ICityApplicationService _service;
        private ITemperatureApplicationService _temperatureService;
        private IHttpAgent _userAgent;

        public CitiesController(ICityApplicationService service, ITemperatureApplicationService temperatureService
            , IHttpAgent userAgent)
        {
            _service = service;
            _temperatureService = temperatureService;
            _userAgent = userAgent;
        }

        [HttpGet]
        [Route("{cityName}/temperatures")]
        public async Task<ActionResult> Get(string cityName)
        {
            var temperatures = await _temperatureService.Find(cityName);

            var obj = new
            {
                City = cityName,
                Temperatures = temperatures.Select(t => new { Date = t.Date, Temperature = t.Measure })
            };

            return Ok(obj);
        }

        [HttpPost]
        [Route("{cityName}")]
        public async Task<ActionResult> Post(string cityName)
        {
            var id = await _service.Create(cityName);
            return Created($"api/cities/{cityName}/{id}", id);
        }

        [HttpPost]
        [Route("by_cep/{CEP}")]
        public async Task<ActionResult> PostByCep(int CEP)
        {
            if (CEP.ToString().Length != 8)
                throw new Exception(StoneApplicationResources.InvalidCEP);

            var cepModel = await _userAgent.GetAsync<ViaCepModel>(string.Format(StoneApplicationResources.APICEP, CEP));

            var id = await _service.Create(cepModel.Localidade);
            return Created($"api/cities/by_cep/{CEP}/{id}", id);
        }

        [HttpDelete]
        [Route("{cityName}")]
        public async Task<ActionResult> Delete(string cityName)
        {
            await _service.Delete(x => x.Name == cityName);
            return NoContent();
        }

        [HttpDelete]
        [Route("{cityName}/temperatures")]
        public async Task<ActionResult> DeleteTemperatures(string cityName)
        {
            await _temperatureService.Delete(x => x.City.Name == cityName);

            return NoContent();
        }
    }
}