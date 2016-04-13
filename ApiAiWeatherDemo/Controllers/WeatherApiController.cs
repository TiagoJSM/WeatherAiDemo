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

        protected HttpResponseMessage QueryWeather(string question)
        {
            var weatherResponse = _weatherService.Query(question);
            if (weatherResponse == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            if (weatherResponse.ForecastResult.error != null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, weatherResponse);
            }
            return Request.CreateResponse(HttpStatusCode.OK, weatherResponse);
        }
    }
}
