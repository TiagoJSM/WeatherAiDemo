using AiServices.Models.Luis;
using AiServices.Services.AiServices;
using AiServices.Services.Forecast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiServices.Services.WeatherServices
{
    public class LuisWeatherService : BaseWeatherService<LuisQueryResponse>, ILuisWeatherService
    {
        public LuisWeatherService(LuisService aiService, ForecastService forecastService)
            : base(forecastService, aiService)
        {
        }

        protected override string GetLocationFromResponse(LuisQueryResponse queryResponse)
        {
            return queryResponse.entities[0].entity;
        }
    }
}
