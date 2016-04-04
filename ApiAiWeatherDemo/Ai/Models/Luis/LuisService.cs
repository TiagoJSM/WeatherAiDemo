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

        QueryResponse IAiService.Query(string query)
        {
            throw new NotImplementedException();
        }
    }
}