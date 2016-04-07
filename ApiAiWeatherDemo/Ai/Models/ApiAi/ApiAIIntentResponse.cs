using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiAiWeatherDemo.Ai.Models.ApiAi
{
    public class IntentResponses
    {
        public List<ApiAIIntentResponse> responses { get; set; }
    }

    public class ApiAIIntentResponse
    {
        public String id { get; set; }
        public String name { get; set; }
        public List<String> contextIn { get; set; }
        public List<String> contextOut { get; set; }
        public List<String> actions { get; set; }
    }
}