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
    public class WatsonApiController : ApiController
    {
        private WatsonService _aiService = new WatsonService();
        private ForecastService _forecastService = new ForecastService();

        [Route("ask")]
        [HttpPost]
        public IHttpActionResult Post(WeatherQuestionModel model)
        {
            var aiResponse = _aiService.Query(model.Question);
            var city = default(string);
            if (aiResponse != null)
            {
                city = aiResponse.Response.First();
                var forecastResponse = _forecastService.GetFromCity(city);
                model.City = city;
                model.ForecastResult = forecastResponse;
                model.WatsonResponse = aiResponse;
                if (forecastResponse.current == null)
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
            return Ok(model);
        }
    }
}