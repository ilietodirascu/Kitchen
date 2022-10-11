using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Kitchen.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger _logger;

        public TestController(IHttpClientFactory clientFactory, ILogger<TestController> logger)
        {
            _clientFactory = clientFactory;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            _logger.LogInformation("I am here");
            var client = _clientFactory.CreateClient();
            HttpResponseMessage response = await client.GetAsync("http://host.docker.internal:60500/Weatherforecast");
            //HttpResponseMessage response = await client.GetAsync("http://host.docker.internal:60500/Weatherforecast");
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
            })
            .ToArray();
        }
    }
}
