using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AiServices.Models.ApiAi
{
    public class Parameter
    {
        public String name { get; set; }
        public String value { get; set; }
    }

    public class Context
    {
        public String name { get; set; }
        public int lifespan { get; set; }
    }

    public class Response
    {
        public String action { get; set; }
        public List<Context> affectedContexts { get; set; }
        public List<Parameter> parameters { get; set; }
    }

    public class ApiAIIntentObject
    {
        public String id { get; set; }
        public String name { get; set; }
        public List<Context> contexts { get; set; }
        public List<String> templates { get; set; }
        public List<Response> responses { get; set; }
    }
}