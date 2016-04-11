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
        private ApiAiService _aiService;
        private ForecastService _forecastService;
        public ApiAiWeatherService(ApiAiService aiService, ForecastService forecastService)
            : base(forecastService, aiService)
        {
            _aiService = aiService;
            _forecastService = forecastService;
        }

        /*public QueryResponse Query(string question)
        {
            var aiResponse = _aiService.Query(question);

            if (aiResponse == null)
            {
                return null;
            }

            var city = aiResponse.Result.Parameters.GeoCity;
            var forecastResponse = _forecastService.GetFromCity(city);
            var response = new QueryResponse();

            if (forecastResponse.current == null)
            {
                return new QueryResponse()
                {
                    City = city,
                    ForecastResult = null,
                    AiResponse = aiResponse
                };
            }

            return new QueryResponse()
            {
                City = forecastResponse.location.name,
                ForecastResult = forecastResponse,
                AiResponse = aiResponse
            };
        }*/

        protected override string GetLocationFromResponse(QueryApiResponse queryResponse)
        {
            return queryResponse.Result.Parameters.GeoCity;
        }
    }
}
