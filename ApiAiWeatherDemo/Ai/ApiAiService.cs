using ApiAiWeatherDemo.Ai.Models;
using ApiAiWeatherDemo.Ai.Models.ApiAi;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiAiWeatherDemo.Ai
{
    public class ApiAiService : IAiService
    {
        public QueryResponse Query(string query)
        {
            var client = new RestClient("https://api.api.ai/v1/");
            var request = new RestRequest(@"query?v=20150910&query={query}&lang=en&sessionId=1234567890", Method.GET);
            request.AddUrlSegment("query", query);

            request.AddHeader("Authorization", "Bearer 589e7b5ab7ce471db071a1d286e57a85");
            request.AddHeader("ocp-apim-subscription-key", "a3703095-c5b3-4d8d-9e2f-bc1b2b92fc2a5");

            var response = client.Execute<QueryApiResponse>(request);
            var content = response.Data;
            
            if (content.Status.Code != 200)
            {
                return null;
            }

            var city = content.Result.Parameters.GeoCity;
            if(city == null || content.Result.Action != "getWeather")
            {
                return null;
            }
            return new QueryResponse()
            {
                CityName = city
            };
        }
    }
}