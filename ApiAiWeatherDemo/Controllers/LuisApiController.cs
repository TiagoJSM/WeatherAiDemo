﻿using AiServices;
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
    [RoutePrefix("api/luis")]
    public class LuisApiController : WeatherApiController
    {
        //Change the service you want to use
        private LuisService _aiService;
        private ILuisWeatherService _weatherService;

        public LuisApiController(ILuisWeatherService weatherService)
            : base(weatherService)
        {
            _weatherService = weatherService;
        }

        [Route("ask")]
        [HttpPost]
        public IHttpActionResult Post(WeatherQuestionModel model)
        {
            return QueryWeather(model.Question);
        }
    }
}
