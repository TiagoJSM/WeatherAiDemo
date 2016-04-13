using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiAiWeatherDemo.Models;
using AiServices.Services.Forecast;
using AiServices.Ai;
using System.Diagnostics;

namespace AiServices.Services.WeatherServices
{
    public abstract class BaseWeatherService<TQueryResponse> : IWeatherService where TQueryResponse : class
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
            TQueryResponse aiResponse = null;
            var aiExecutionTime = GetExecutionTime(() => aiResponse = _aiService.Query(question));

            if (aiResponse == null)
            {
                return null;
            }

            var city = default(string);
            var forecastExecutionTime = GetExecutionTime(() => city = GetLocationFromResponse(aiResponse));
            if (city == null)
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
                    AiResponse = aiResponse,
                    AiExecutionTime = aiExecutionTime
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
                AiResponse = aiResponse,
                AiExecutionTime = aiExecutionTime,
                ForecastExecutionTime = forecastExecutionTime
            };
        }

        protected abstract string GetLocationFromResponse(TQueryResponse queryResponse);

        private TimeSpan GetExecutionTime(Action action)
        {
            var aiWatch = Stopwatch.StartNew();
            action();
            aiWatch.Stop();
            return aiWatch.Elapsed;
        }
    }
}
