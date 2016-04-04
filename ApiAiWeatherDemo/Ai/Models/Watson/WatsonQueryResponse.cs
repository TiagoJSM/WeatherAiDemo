﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiAiWeatherDemo.Ai.Models.Watson
{
    public class WatsonQueryResponse
    {
        public int ConversationId { get; set; }
        public int ClientId { get; set; }
        public string Input { get; set; }
        public float Confidence { get; set; }
        public List<string> Response { get; set; }
    }
}