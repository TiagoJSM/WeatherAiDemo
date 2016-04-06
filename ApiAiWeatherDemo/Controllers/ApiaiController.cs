using ApiAiWeatherDemo.Ai;
using ApiAiWeatherDemo.Ai.Models.ApiAi;
using ApiAiWeatherDemo.Forecast;
using ApiAiWeatherDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ApiAiWeatherDemo.Controllers
{
    public class ApiaiController : Controller
    {
        //Change the service you want to use
        private ApiAiService _aiService = new ApiAiService();
        private ForecastService _forecastService = new ForecastService();

        // GET: Weather
        public ActionResult Index(string city)
        {
            var model = new WeatherQuestionModel();
            if(city != null)
            {
                var forecastResponse = _forecastService.GetFromCity(city);
                model.City = city;
                model.ForecastResult = forecastResponse;
            }
            if (TempData["aiResponse"] != null)
            {
                model.ApiAIResponse = (QueryApiResponse)TempData["aiResponse"];
            }

            return View(model);
        }

        public ActionResult Post(WeatherQuestionModel model)
        {
            var aiResponse = _aiService.Query(model.Question);
            var city = default(string);
            if(aiResponse != null)
            {
                city = aiResponse.Result.Parameters.GeoCity;
                TempData["aiResponse"] = aiResponse;
            }
            return RedirectToAction("Index", new { city = city });
        }

        public ActionResult Intents()
        {
            IntentResponses aiResponse = _aiService.getIntents();
            
            return View(aiResponse.responses);
        }
    }
}