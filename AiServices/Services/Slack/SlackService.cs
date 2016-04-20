using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiServices.Services.Slack
{
    public class SlackService
    {
        public void postMessage(SlackMessage message)
        {
            var client = new RestClient("https://hooks.slack.com/");
            var request = new RestRequest(@"services/{webhook}", Method.POST);
            request.AddUrlSegment("webhook", "");

            request.AddHeader("Content-type", "application/json");

            request.AddBody(message);

            var response = client.Execute<HttpResponse>(request);
            var responseData = response.Data;
        }
    }
}
