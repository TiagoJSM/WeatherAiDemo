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
        private ILuisContextSession _contextSession;
        private Dictionary<string, Func<LuisQueryResponse, string>> _intentMapping;

        public LuisWeatherService(LuisService aiService, ForecastService forecastService, ILuisContextSession contextSession)
            : base(forecastService, aiService)
        {
            _contextSession = contextSession;
            _intentMapping = new Dictionary<string, Func<LuisQueryResponse, string>>()
                {
                    ["getWeather"] = GetWeather,
                    ["getWeatherContext"] = GetWeatherContext
            };
        }

        protected override string GetLocationFromResponse(LuisQueryResponse queryResponse)
        {
            var intent = queryResponse.intents.FirstOrDefault();
            if(intent == null)
            {
                return null;
            }

            Func<LuisQueryResponse, string> resolver;
            if (_intentMapping.TryGetValue(intent.intent, out resolver))
            {
                return resolver(queryResponse);
            }
            return null;
        }

        private string GetWeather(LuisQueryResponse queryResponse)
        {
            var location = "";

            if (queryResponse.entities.Length == 0)
            {
                location = "";
            }
            else if (queryResponse.entities.Length > 0)
            {
                location = queryResponse.entities[0].entity;
                _contextSession.SaveContext(new LuisContext() { Location = location });
            }

            return location;
        }

        private string GetWeatherContext(LuisQueryResponse queryResponse)
        {
            return _contextSession.GetContext()?.Location;   
        }
    }
}
