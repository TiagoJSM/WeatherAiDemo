using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AiServices.Models.ApiAi
{
    public class IntentResponses
    {
        public List<ApiAIIntentResponse> responses { get; set; }
    }

    public class ContextOut
    {
        public String name { get; set; }
        public int lifespan { get; set; }
    }

    public class ApiAIIntentResponse
    {
        public String id { get; set; }
        public String name { get; set; }
        public List<String> contextIn { get; set; }
        public List<ContextOut> contextOut { get; set; }
        public List<String> actions { get; set; }
    }
}