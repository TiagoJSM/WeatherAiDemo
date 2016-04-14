using AiServices.Models.ApiAi;
using AiServices.Models.Luis;
using AiServices.Models.Watson;
using AiServices.Services.Forecast.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ApiAiWeatherDemo.Models
{
    public class WeatherQuestionModel
    {
        [DisplayName("Question")]
        public string Question { get; set; }
        public string City { get; set; }
        public CityForecastModel ForecastResult { get; set; }
        public LuisQueryResponse LuisResponse { get; set; }
        public QueryApiResponse ApiAIResponse { get; set; }
        public WatsonConversationResponse WatsonResponse { get; set; }
    }
}