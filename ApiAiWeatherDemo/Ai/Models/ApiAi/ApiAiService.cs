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
        public QueryApiResponse Query(string query, string sessionId)
        {
            var client = new RestClient("https://api.api.ai/v1/");
            var request = new RestRequest(@"query?v=20150910&query={query}&lang=en&sessionId={sessionId}", Method.GET);
            request.AddUrlSegment("query", query);
            request.AddUrlSegment("sessionId", sessionId);

            request.AddHeader("Authorization", "Bearer 589e7b5ab7ce471db071a1d286e57a85");
            request.AddHeader("ocp-apim-subscription-key", "a3703095-c5b3-4d8d-9e2f-bc1b2b92fc2a5");

            var response = client.Execute<QueryApiResponse>(request);
            var responseData = response.Data;

            if (responseData.Status.Code != 200)
            {
                return null;
            }
            if(responseData.Result.Score < 0.90)
            {
                return null;
            }

            return responseData;
        }

        QueryResponse IAiService.Query(string query, string sessionId)
        {
            throw new NotImplementedException();
        }

        public ApiAIIntentObject getIntents()
        {
            var client = new RestClient("https://api.api.ai/v1/");
            var request = new RestRequest(@"intents?v=20150910", Method.GET);

            request.AddHeader("Authorization", "Bearer 589e7b5ab7ce471db071a1d286e57a85");
            request.AddHeader("ocp-apim-subscription-key", "a3703095-c5b3-4d8d-9e2f-bc1b2b92fc2a5");

            var response = client.Execute<IntentResponses>(request);
            IntentResponses intentsResponse = new IntentResponses();
            intentsResponse.responses = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ApiAIIntentResponse>>(response.Content);

            var requestSingleIntent = new RestRequest(@"intents/{id}?v=20150910", Method.GET);
            requestSingleIntent.AddUrlSegment("id", intentsResponse.responses.First().id);

            requestSingleIntent.AddHeader("Authorization", "Bearer 589e7b5ab7ce471db071a1d286e57a85");
            requestSingleIntent.AddHeader("ocp-apim-subscription-key", "a3703095-c5b3-4d8d-9e2f-bc1b2b92fc2a5");

            var singleIntentResponse = client.Execute<ApiAIIntentObject>(requestSingleIntent);

            ApiAIIntentObject result = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiAIIntentObject>(singleIntentResponse.Content);

            return result;
        }
    }
}