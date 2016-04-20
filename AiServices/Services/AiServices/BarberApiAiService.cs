using AiServices.Ai;
using AiServices.Models.ApiAi;
using AiServices.Models.Slack;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AiServices.Services.AiServices
{
    public class BarberApiAiService
    {
        private ApiAiUserSettings _settings;

        public BarberApiAiService(ApiAiUserSettings settings)
        {
            _settings = settings;
        }

        public QueryApiResponse Query(SlackOutgoingData query)
        {
            var client = new RestClient("https://api.api.ai/v1/");
            var request = new RestRequest(@"query?v=20150910&query={query}&lang=en&sessionId={sessionId}", Method.GET);
            request.AddUrlSegment("query", query.text);
            request.AddUrlSegment("sessionId", query.user_id);

            request.AddHeader("Authorization", "Bearer 20f8ed94bf1e43e0887266818d1314a1");
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
    }
}