using ApiAiWeatherDemo.Ai;
using ApiAiWeatherDemo.Ai.Models.ApiAi;
using ApiAiWeatherDemo.Forecast;
using ApiAiWeatherDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiAiWeatherDemo.Controllers
{
    [RoutePrefix("api/apiai")]
    public class ApiAiApiController : ApiController
    {
        //Change the service you want to use
        private ApiAiService _aiService = new ApiAiService();
        private ForecastService _forecastService = new ForecastService();

        [Route("ask")]
        [HttpPost]
        public IHttpActionResult Post(WeatherQuestionModel model)
        {
            var aiResponse = _aiService.Query(model.Question);
            var city = default(string);
            if (aiResponse != null)
            {
                city = aiResponse.Result.Parameters.GeoCity;
                var forecastResponse = _forecastService.GetFromCity(city);
                model.City = city;
                model.ForecastResult = forecastResponse;
            }
            else
            {
                return NotFound();
            }
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
    }
}
