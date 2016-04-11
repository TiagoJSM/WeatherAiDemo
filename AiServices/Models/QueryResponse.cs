using AiServices.Services.Forecast.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiAiWeatherDemo.Models
{
    public class QueryResponse
    {
        public string City { get; set; }
        public object AiResponse { get; set; }
        public CityForecastModel ForecastResult { get; set; }
    }
}