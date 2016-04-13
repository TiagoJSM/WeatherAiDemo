using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiServices.Services.AiServices;
using ApiAiWeatherDemo.Models;
using AiServices.Services.Forecast;
using AiServices.Services.WeatherServices;
using AiServices.Models.ApiAi;

namespace AiServices.Services.WeatherServices
{
    public class ApiAiWeatherService : BaseWeatherService<QueryApiResponse>, IApiAiWeatherService
    {
        public ApiAiWeatherService(ApiAiService aiService, ForecastService forecastService)
            : base(forecastService, aiService)
        {
        }

        protected override string GetLocationFromResponse(QueryApiResponse queryResponse)
        {
            return queryResponse.Result?.Parameters?.GeoCity;
        }
    }
}
