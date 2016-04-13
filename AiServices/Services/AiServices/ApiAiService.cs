using AiServices.Ai;
using AiServices.Models.ApiAi;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AiServices.Services.AiServices
{
    public class ApiAiService : IAiService<QueryApiResponse>
    {
        private ApiAiUserSettings _settings;

        public ApiAiService(ApiAiUserSettings settings)
        {
            _settings = settings;
        }

        public QueryApiResponse Query(string query)
        {
            var client = new RestClient("https://api.api.ai/v1/");
            var request = new RestRequest(@"query?v=20150910&query={query}&lang=en&sessionId={sessionId}", Method.GET);
            request.AddUrlSegment("query", query);
            request.AddUrlSegment("sessionId", _settings.SessionId);

            request.AddHeader("Authorization", "Bearer 589e7b5ab7ce471db071a1d286e57a85");
            request.AddHeader("ocp-apim-subscription-key", "a3703095-c5b3-4d8d-9e2f-bc1b2b92fc2a5");

            var response = client.Execute<QueryApiResponse>(request);
            var responseData = response.Data;

            if (responseData.Status.Code != 200)
            {
                return responseData;
            }
            if(responseData.Result.Score < 0.90)
            {
                return responseData;
            }

            return responseData;
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

        public ApiAiStatusObject updateIntent(ApiAIIntentObject intent)
        {
            var client = new RestClient("https://api.api.ai/v1/");
            var request = new RestRequest(@"intents/{id}?v=20150910", Method.PUT);
            request.AddUrlSegment("id", intent.id);
            request.AddHeader("Authorization", "Bearer 0d5d0cae1cc1475ba2dd556326dd5587"); //Developer access token 0d5d0cae1cc1475ba2dd556326dd5587
            request.AddHeader("ocp-apim-subscription-key", "a3703095-c5b3-4d8d-9e2f-bc1b2b92fc2a5");
            request.AddHeader("Content-Type", "application/json; charset=utf-8");
            request.AddHeader("Accept", "application/json");
            //request.JsonSerializer.ContentType = "application/json; charset=utf-8";
            request.AddBody(Newtonsoft.Json.JsonConvert.SerializeObject(intent));
            //request.AddParameter("application/json", , ParameterType.RequestBody);

            var singleIntentResponse = client.Execute<ApiAiStatusObject>(request);

            ApiAiStatusObject result = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiAiStatusObject>(singleIntentResponse.Content);

            return result;
        }

    }
}