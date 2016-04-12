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
        public WatsonUserSettings GetUserSettings()
        {
            var client = new RestClient("https://gateway.watsonplatform.net")
            {
                Authenticator = new HttpBasicAuthenticator("200717c0-b5d8-4d36-8409-ff6eeef50a9a", "hoevZzE8cedp")
            };
            var request = new RestRequest(@"dialog/api/v1/dialogs/1ae4bebe-3c6c-4968-9a48-80ce5c8566c8/conversation", Method.POST);
            var response = client.Execute<WatsonQueryResponse>(request);

            if (response.StatusCode != HttpStatusCode.Created)
            {
                return null;
            }

            return new WatsonUserSettings(response.Data.ClientId, response.Data.ConversationId);
        }
    }
}
