using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestSharp;
using AiServices.Models.Luis;
using RestSharp.Extensions.MonoHttp;
using AiServices.Ai;

namespace AiServices.Services.AiServices
{
    public class LuisService : IAiService<LuisQueryResponse>
    {
        /*private ILuisContextSession _contextSession;

        public LuisService(ILuisContextSession contextSession)
        {
            _contextSession = contextSession;
        }*/

        public LuisQueryResponse Query(string query)
        {
            query = HttpUtility.HtmlEncode(query);
            var client = new RestClient("https://api.projectoxford.ai/luis/v1/");
            var request = new RestRequest(@"application?id=751d9b3a-17cd-47e7-be98-323c19646648&subscription-key={key}&q={query}", Method.GET);
            request.AddUrlSegment("key", "55608ef7c8a247749f665b77bdfb8b46");
            request.AddUrlSegment("query", query);

            var response = client.Execute<LuisQueryResponse>(request);
            var content = response.Content;

            var res = Newtonsoft.Json.JsonConvert.DeserializeObject<LuisQueryResponse>(content);

            var intent = res.intents.FirstOrDefault(item => item.intent == "getWeather" || item.intent == "getWeatherContext");

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

        public List<LuisIntentSample> getIntentSamples()
        {
            var client = new RestClient("https://api.projectoxford.ai/luis/v1.0/");
            var request = new RestRequest(@"prog/apps/{app-id}/intents/{intent-id}/sample?maxQueriesCount={query-count}", Method.GET);
            request.AddUrlSegment("app-id", "751d9b3a-17cd-47e7-be98-323c19646648");
            request.AddUrlSegment("intent-id", "f9ba71ff-ae55-4eb1-8fd8-a3f701421100");
            request.AddUrlSegment("query-count", "50");

            request.AddHeader("ocp-apim-subscription-key", "55608ef7c8a247749f665b77bdfb8b46");

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