using AiServices.Ai;
using AiServices.Models.Watson;
using ApiAiWeatherDemo.Models;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace AiServices.Services.AiServices
{
    public class WatsonService : IAiService<WatsonQueryResponse>
    {
        private WatsonUserSettings _userSettings;

        public WatsonService(WatsonUserSettings userSettings)
        {
            _userSettings = userSettings;
        }

        public WatsonQueryResponse Query(string question)
        {
            var client = new RestClient("https://gateway.watsonplatform.net")
            {
                Authenticator = new HttpBasicAuthenticator("200717c0-b5d8-4d36-8409-ff6eeef50a9a", "hoevZzE8cedp")
            };
            var request = new RestRequest(@"dialog/api/v1/dialogs/1ae4bebe-3c6c-4968-9a48-80ce5c8566c8/conversation", Method.POST);
            /*var response = client.Execute<WatsonQueryResponse>(request);

            if (response.StatusCode != HttpStatusCode.Created)
            {
                return null;
            }*/

            request.AddParameter("client_id", _userSettings.ClientId);
            request.AddParameter("conversation_id", _userSettings.ConversationId);
            request.AddParameter("input", question);

            var response = client.Execute<WatsonQueryResponse>(request);
            WatsonQueryResponse res = response.Data;
            
            if (response.StatusCode != HttpStatusCode.Created)
            {
                return null;
            }

            if (res.Confidence < 0.9)
            {
                return null;
            }

            var city = res.Response.First();

            return res;
        }
    }
}