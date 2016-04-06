using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace ApiAiWeatherDemo.Ai.Models.Watson
{
    public class WatsonService : IAiService
    {
        //public QueryResponse Query(WatsonQueryRequest userRequest)
        public WatsonQueryResponse Query(string query)
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

            

            request.AddParameter("client_id", response.Data.ClientId);
            request.AddParameter("conversation_id", response.Data.ConversationId);
            request.AddParameter("input", query);

            response = client.Execute<WatsonQueryResponse>(request);
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

        QueryResponse IAiService.Query(string query)
        {
            throw new NotImplementedException();
        }
    }
}