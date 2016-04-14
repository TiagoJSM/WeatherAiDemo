using AiServices.Models.Watson;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AiServices.Services.AiServices
{
    public class WatsonLoginService : IWatsonLoginService
    {
        private WatsonData _data;
        public WatsonLoginService(WatsonData data)
        {
            _data = data;
        }

        public WatsonUserSettings GetUserSettings()
        {
            var client = new RestClient("https://gateway.watsonplatform.net")
            {
                Authenticator = new HttpBasicAuthenticator(_data.Username, _data.Password)
            };
            var request = new RestRequest(@"dialog/api/v1/dialogs/{dialogueId}/conversation", Method.POST);
            request.AddUrlSegment("dialogueId", _data.DialogueId);
            var response = client.Execute<WatsonQueryResponse>(request);

            if (response.StatusCode != HttpStatusCode.Created)
            {
                return null;
            }

            return new WatsonUserSettings(response.Data.ClientId, response.Data.ConversationId);
        }
    }
}
