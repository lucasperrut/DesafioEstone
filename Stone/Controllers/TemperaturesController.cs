using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stone.Application.Interfaces;
using System.Linq;
using System;

namespace Stone.Controllers
{
    [Produces("application/json")]
    [Route("api/temperatures")]
    public class TemperaturesController : Controller
    {
        private ICityApplicationService _cityService;

        public TemperaturesController(ICityApplicationService service)
        {
            _cityService = service;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var oData = Request.Query;
            var page = oData.ContainsKey("page") && Convert.ToInt32(oData["page"][0]) > 0 ? Convert.ToInt32(oData["page"][0]) : 1;

            var cities = await _cityService.GetAll(page);

            var result = new
            {
                page = page,
                count = cities.Count(),
                data = cities.Select(c => new { ID = c.ID, Name = c.Name })
            };

            return Ok(result);
        }
    }
}