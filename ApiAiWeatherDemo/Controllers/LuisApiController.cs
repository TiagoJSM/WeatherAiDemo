using AiServices;
using AiServices.Services.AiServices;
using AiServices.Services.Forecast;
using AiServices.Models.Luis;
using ApiAiWeatherDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiAiWeatherDemo.Controllers
{
    [RoutePrefix("api/luis")]
    public class LuisApiController : WeatherApiController
    {
        //Change the service you want to use
        private LuisService _aiService;
        private ILuisWeatherService _weatherService;

        public LuisApiController(LuisService aiService, ILuisWeatherService weatherService)
            : base(weatherService)
        {
            _aiService = aiService;
            _weatherService = weatherService;
        }

        [Route("ask")]
        [HttpPost]
        public IHttpActionResult Post(WeatherQuestionModel model)
        {
            return QueryWeather(model.Question);
        }

        [Route("intents")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            List<LuisIntentSample> aiResponse = _aiService.getIntentSamples();
            if (aiResponse == null)
            {
                return NotFound();
            }
            return Ok(aiResponse);
        }
    }
}
