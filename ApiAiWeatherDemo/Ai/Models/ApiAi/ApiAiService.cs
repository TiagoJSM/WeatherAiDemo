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
        public QueryApiResponse Query(string query)
        {
            var client = new RestClient("https://api.api.ai/v1/");
            var request = new RestRequest(@"query?v=20150910&query={query}&lang=en&sessionId=1234567890", Method.GET);
            request.AddUrlSegment("query", query);

            request.AddHeader("Authorization", "Bearer 589e7b5ab7ce471db071a1d286e57a85");
            request.AddHeader("ocp-apim-subscription-key", "a3703095-c5b3-4d8d-9e2f-bc1b2b92fc2a5");

            var response = client.Execute<QueryApiResponse>(request);
            QueryApiResponse res = response.Data;
            if (res.Status.Code != 200)
            {
                return null;
            }

            if(res.Result.Score < 0.75)
            {
                return null;
            }

            if(res.Result.Parameters.GeoCity == null || res.Result.Action != "getWeather")
            {
                return null;
            }
            return res;
        }

        QueryResponse IAiService.Query(string query)
        {
            throw new NotImplementedException();
        }

        public IntentResponses getIntents()
        {
            var client = new RestClient("https://api.api.ai/v1/");
            var request = new RestRequest(@"intents?v=20150910", Method.GET);

            request.AddHeader("Authorization", "Bearer 589e7b5ab7ce471db071a1d286e57a85");
            request.AddHeader("ocp-apim-subscription-key", "a3703095-c5b3-4d8d-9e2f-bc1b2b92fc2a5");

            var response = client.Execute<IntentResponses>(request);
            IntentResponses result = new IntentResponses();
            result.responses = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ApiAIIntentResponse>>(response.Content);
            return result;
        }
    }
}