using AiServices.Ai;
using AiServices.Models.Watson;
using ApiAiWeatherDemo.Models;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Xml.Linq;

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

            request.AddParameter("client_id", _userSettings.ClientId);
            request.AddParameter("conversation_id", _userSettings.ConversationId);
            request.AddParameter("input", question);

            var response = client.Execute<WatsonQueryResponse>(request);
            WatsonQueryResponse res = response.Data;
            
            if (response.StatusCode != HttpStatusCode.Created)
            {
                return res;
            }

            if (res.Confidence < 0.9)
            {
                return res;
            }

            var city = res.Response.First();

            return res;
        }

        public String getWatsonSchema()
        {
            string result = string.Empty;

            using (Stream stream = this.GetType().Assembly.
                       GetManifestResourceStream("AiServices.Models.Watson." + "WatsonWeather.xml"))
            {
                using (StreamReader sr = new StreamReader(stream))
                {
                    result = sr.ReadToEnd();
                }
            }
            return result;
        }
    }
}