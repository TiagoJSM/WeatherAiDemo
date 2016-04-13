using AiServices;
using AiServices.Services.AiServices;
using AiServices.Services.Forecast;
using ApiAiWeatherDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiAiWeatherDemo.Controllers
{
    [RoutePrefix("api/watson")]
    public class WatsonApiController : WeatherApiController
    {
        private WatsonService _aiService;
        private IWatsonWeatherService _weatherService;

        public WatsonApiController(WatsonService aiService, IWatsonWeatherService weatherService)
            : base(weatherService)
        {
            _aiService = aiService;
            _weatherService = weatherService;
        }

        [Route("ask")]
        [HttpPost]
        public HttpResponseMessage Post(WeatherQuestionModel model)
        {
            return QueryWeather(model.Question);
        }
    }
}