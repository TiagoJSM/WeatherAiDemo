using AiServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiAiWeatherDemo.Controllers
{
    public abstract class WeatherApiController : ApiController
    {
        private IWeatherService _weatherService;

        public WeatherApiController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        protected IHttpActionResult QueryWeather(string question)
        {
            var weatherResponse = _weatherService.Query(question);
            if (weatherResponse == null)
            {
                return BadRequest();
            }
            if (weatherResponse.ForecastResult == null)
            {
                return NotFound();
            }
            return Ok(weatherResponse);
        }
    }
}
