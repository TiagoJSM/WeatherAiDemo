﻿using AiServices.Services.AiServices;
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
    [RoutePrefix("api/luis")]
    public class LuisApiController : ApiController
    {
        //Change the service you want to use
        private LuisService _aiService;
        private ForecastService _forecastService = new ForecastService();

        public LuisApiController(LuisService aiService)
        {
            _aiService = aiService;
        }

        [Route("ask")]
        [HttpPost]
        public IHttpActionResult Post(WeatherQuestionModel model)
        {
            var aiResponse = _aiService.Query(model.Question);
            var city = default(string);
            if (aiResponse != null)
            {
                city = aiResponse.entities[0].entity;
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
    }
}
