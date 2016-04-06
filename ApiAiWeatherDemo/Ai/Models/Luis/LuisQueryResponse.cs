using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiAiWeatherDemo.Ai.Models.Luis
{
    public class LuisQueryResponse
    {
        public String query { get; set; }
        public Intent[] intents { get; set; }
        public Entity[] entities { get; set; }
    }

    public class Entity
    {
        public String entity { get; set; }
        public String type { get; set; }
        public int startIndex { get; set; }
        public int endIndex { get; set; }
        public float score { get; set; }
    }

    public class Intent
    {
        public String intent { get; set; }
        public float score { get; set; }
        public String action { get; set; }
    }
}