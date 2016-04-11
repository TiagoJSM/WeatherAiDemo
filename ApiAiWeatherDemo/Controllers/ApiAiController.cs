using ApiAiWeatherDemo.Ai;
using ApiAiWeatherDemo.Ai.Models.ApiAi;
using ApiAiWeatherDemo.Forecast;
using ApiAiWeatherDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ApiAiWeatherDemo.Extensions;
using ApiAiWeatherDemo.Forecast.Models;

namespace ApiAiWeatherDemo.Controllers
{
    [RoutePrefix("api/apiai")]
    public class ApiAiController : ApiController
    {
        //Change the service you want to use
        private ApiAiService _aiService = new ApiAiService();
        private ForecastService _forecastService = new ForecastService();

        [Route("ask")]
        [HttpPost]
        public IHttpActionResult Post(WeatherQuestionModel model)
        {
            var settings = GetUserSettings();
            var aiResponse = _aiService.Query(model.Question, settings.ApiAiSessionId);

            if (aiResponse == null)
            {
                return BadRequest();
            }

            var city = aiResponse.Result.Parameters.GeoCity;
            var forecastResponse = _forecastService.GetFromCity(city);

            if (forecastResponse.current == null)
            {
                return NotFound();
            }

            model.City = forecastResponse.location.name;
            model.ForecastResult = forecastResponse;
            model.ApiAIResponse = aiResponse;
            model.ForecastResult = forecastResponse;

            return Ok(model);
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

        private UserSettings GetUserSettings()
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
        }
    }
}
