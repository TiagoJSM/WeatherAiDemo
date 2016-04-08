using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApiAiWeatherDemo.Ai.Models;
using RestSharp;
using ApiAiWeatherDemo.Ai.Models.Luis;

namespace ApiAiWeatherDemo.Ai
{
    public class LuisService : IAiService
    {
        public LuisQueryResponse Query(string query)
        {
            query = HttpUtility.HtmlEncode(query);
            var client = new RestClient("https://api.projectoxford.ai/luis/v1/");
            var request = new RestRequest(@"application?id=858a25c8-f21f-47fd-8b1f-80b3a051b1d5&subscription-key={key}&q={query}", Method.GET);
            request.AddUrlSegment("key", "97c75fab726c47f685fb964878f5fac4");
            request.AddUrlSegment("query", query);

            var response = client.Execute<LuisQueryResponse>(request);
            var content = response.Content;

            LuisQueryResponse res = Newtonsoft.Json.JsonConvert.DeserializeObject<LuisQueryResponse>(content);


            Intent intent = null;
            intent = res.intents.FirstOrDefault(item => item.intent == "getWeather");

            if (intent == null)
            {
                return null;
            }

            //If the confidence is too low we return null as the ai might have returned a false intent
            if(res.intents[0].score < 0.75)
            {
                return null;
            }

            return res;
        }

        QueryResponse IAiService.Query(string query, string sessionId)
        {
            throw new NotImplementedException();
        }

        public List<LuisIntentSample> getIntentSamples()
        {
            var client = new RestClient("https://api.projectoxford.ai/luis/v1.0/");
            var request = new RestRequest(@"prog/apps/{app-id}/intents/{intent-id}/sample?maxQueriesCount={query-count}", Method.GET);
            request.AddUrlSegment("app-id", "858a25c8-f21f-47fd-8b1f-80b3a051b1d5");
            request.AddUrlSegment("intent-id", "41ba150c-db61-4ae4-84c5-bc260b3655d2");
            request.AddUrlSegment("query-count", "50");

            request.AddHeader("ocp-apim-subscription-key", "97c75fab726c47f685fb964878f5fac4");

            var response = client.Execute<List<LuisIntentSample>>(request);
            var content = response.Content;
            List<LuisIntentSample> result = new List<LuisIntentSample>();

            result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<LuisIntentSample>>(content);
            return result;
        }

        public LuisAppInfo getAppInfo()
        {
            var client = new RestClient("https://api.projectoxford.ai/luis/v1.0/");
            var request = new RestRequest(@"prog/apps/{app-id}", Method.GET);
            request.AddUrlSegment("app-id", "858a25c8-f21f-47fd-8b1f-80b3a051b1d5");


            request.AddHeader("ocp-apim-subscription-key", "97c75fab726c47f685fb964878f5fac4");

            var response = client.Execute<LuisAppInfo>(request);
            var content = response.Content;
           
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<LuisAppInfo>(content);
            return result;
        }
    }
}