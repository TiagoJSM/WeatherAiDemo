using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiAiWeatherDemo.Models;
using AiServices.Services.Forecast;
using AiServices.Ai;

namespace AiServices.Services.WeatherServices
{
    public abstract class BaseWeatherService<TQueryResponse> : IWeatherService
    {
        private ForecastService _forecastService;
        private IAiService<TQueryResponse> _aiService;

        public BaseWeatherService(ForecastService forecastService, IAiService<TQueryResponse> aiService)
        {
            _forecastService = forecastService;
            _aiService = aiService;
        }

        public QueryResponse Query(string question)
        {
            var aiResponse = _aiService.Query(question);

            if (aiResponse == null)
            {
                return null;
            }
            var city = GetLocationFromResponse(aiResponse);
            if(city == null)
            {
                return null;
            }

            var forecastResponse = _forecastService.GetFromCity(city);
            var response = new QueryResponse();
            if (forecastResponse == null)
            {
                return new QueryResponse()
                {
                    City = "",
                    ForecastResult = null,
                    AiResponse = aiResponse
                };
            }
                if (forecastResponse.current == null)
                {
                    return new QueryResponse()
                    {
                        City = city,
                        ForecastResult = forecastResponse,
                        AiResponse = aiResponse
                    };
                }

            return new QueryResponse()
            {
                City = forecastResponse.location.name,
                ForecastResult = forecastResponse,
                AiResponse = aiResponse
            };
        }

        protected abstract string GetLocationFromResponse(TQueryResponse queryResponse);
    }
}
