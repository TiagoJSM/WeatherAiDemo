using AiServices.Models.Watson;
using AiServices.Services.AiServices;
using AiServices.Services.Forecast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiServices.Services.WeatherServices
{
    public class WatsonWeatherService : BaseWeatherService<WatsonQueryResponse>, IWatsonWeatherService
    {
        public WatsonWeatherService(WatsonService aiService, ForecastService forecastService)
            : base(forecastService, aiService)
        {
        }

        protected override string GetLocationFromResponse(WatsonQueryResponse queryResponse)
        {
            return queryResponse.Response.First();
        }
    }
}
