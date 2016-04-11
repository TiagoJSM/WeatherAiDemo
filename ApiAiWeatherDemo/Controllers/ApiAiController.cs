using ApiAiWeatherDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ApiAiWeatherDemo.Extensions;
using AiServices.Services.AiServices;
using AiServices.Models.ApiAi;
using AiServices;
using AiServices.Services.Forecast;

namespace ApiAiWeatherDemo.Controllers
{
    [RoutePrefix("api/apiai")]
    public class ApiAiController : WeatherApiController
    {
        //Change the service you want to use
        private ApiAiService _aiService;
        private IApiAiWeatherService _weatherService;
        private ForecastService _forecastService = new ForecastService();

        public ApiAiController(ApiAiService aiService, IApiAiWeatherService weatherService)
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
        public IHttpActionResult Intents()
        {
            ApiAIIntentObject aiResponse = _aiService.getIntents();

            if (aiResponse == null)
            {
                return NotFound();
            }

            return Ok(aiResponse);
        }

        /*private UserSettings GetUserSettings()
        {
            var settings = HttpContext.Current.Session.GetUserSettings();
            if(settings == null)
            {
                settings = new UserSettings();
            }
            if (settings.ApiAiSessionId == null)
            {
                settings.ApiAiSessionId = Guid.NewGuid().ToString().Substring(0, 36);   //36 is defined by API.AI
                HttpContext.Current.Session.SaveUserSettings(settings);
            }
            return settings;
        }*/
    }
}
