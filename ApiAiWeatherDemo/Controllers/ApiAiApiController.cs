﻿using ApiAiWeatherDemo.Ai;
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
            if (aiResponse == null)
            {
                return BadRequest();
            }
            var city = aiResponse.Result.Parameters.GeoCity;
            var forecastResponse = _forecastService.GetFromCity(city);
            if(forecastResponse.current == null)
            {
                return NotFound();
            }
            model.City = city;
            model.ForecastResult = forecastResponse;

            return Ok(model);
        }
    }
}