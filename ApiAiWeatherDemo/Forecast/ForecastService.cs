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
            var client = new RestClient("http://api.apixu.com/v1/");
            var request = new RestRequest(@"current.json?key=16a66ee212024cb1bef125700160404&q={city}", Method.GET);
            request.AddUrlSegment("city", city);

            var response = client.Execute<CityForecastModel>(request);
            var content = response.Data;

            return content;
        }
    }
}