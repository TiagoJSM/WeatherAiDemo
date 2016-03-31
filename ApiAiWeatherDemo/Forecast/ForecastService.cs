using ApiAiWeatherDemo.Forecast.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiAiWeatherDemo.Forecast
{
    public class ForecastService
    {
        public CityForecastModel GetFromCity(string city)
        {
            var client = new RestClient("http://api.openweathermap.org/");
            var request = new RestRequest(@"data/2.5/weather?q={city}&units=metric&APPID=4e8f8dcec02080b290bcbcbfc2beaab1", Method.GET);
            request.AddUrlSegment("city", city);

            var response = client.Execute<CityForecastModel>(request);
            var content = response.Data;

            return content;
        }
    }
}