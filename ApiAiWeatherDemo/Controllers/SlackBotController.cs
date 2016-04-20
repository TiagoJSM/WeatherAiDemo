using AiServices.Models.Slack;
using AiServices.Services.AiServices;
using AiServices.Services.Slack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ApiAiWeatherDemo.Controllers
{
    [RoutePrefix("bot/slack")]
    public class SlackBotController : ApiController
    {
        private ApiAiService apiAiService;
        private SlackService slackService;

        [Route("ask")]
        [HttpPost]
        public HttpResponseMessage Post(SlackOutgoingData query)
        {
            var aiResponse = apiAiService.Query(query.text);

            SlackMessage slackmessage = null;
            slackmessage.text = aiResponse.Result.Action + "Called by: " + query.user_name;

            //slackService.postMessage(slackmessage);

            return Request.CreateResponse(HttpStatusCode.OK, slackmessage);
        }
    }
}