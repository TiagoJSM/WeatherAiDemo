using ApiAiWeatherDemo.Forecast.Models;
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
    }
}