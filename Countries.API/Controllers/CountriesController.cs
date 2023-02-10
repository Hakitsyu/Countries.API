using Countries.Core.Commands;
using Countries.Core.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace Countries.API.Controllers
{
    [Controller]
    [Route(URL)]
    public class CountriesController : ControllerBase
    {
        private const string URL = "countries";
        private ICountriesService _service;
        private ITraceRouteService _traceRouteService;

        public CountriesController(ICountriesService service, 
            ITraceRouteService traceRouteService)
            => (_service, _traceRouteService) = (service, traceRouteService);

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
            => Ok(await _service.GetAll());

        [HttpPost("trace-route")]
        public async Task<IActionResult> TraceRoute([FromBody] TraceRouteCommand command)
            => Ok(await _traceRouteService.Trace(command));
    }
}
