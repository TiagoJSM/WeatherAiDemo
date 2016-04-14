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
        private WatsonData _data;

        public WatsonService(WatsonUserSettings userSettings, WatsonData data)
        {
            _userSettings = userSettings;
            _data = data;
        }

        public WatsonQueryResponse Query(string question)
        {
            var client = new RestClient("https://gateway.watsonplatform.net")
            {
                Authenticator = new HttpBasicAuthenticator(_data.Username, _data.Password)
            };
            var request = new RestRequest(@"dialog/api/v1/dialogs/{dialogueId}/conversation", Method.POST);

            request.AddUrlSegment("dialogueId", _data.DialogueId);
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