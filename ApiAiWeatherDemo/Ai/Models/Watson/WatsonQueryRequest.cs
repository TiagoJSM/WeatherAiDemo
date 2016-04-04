﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiAiWeatherDemo.Ai.Models.Watson
{
    public class WatsonQueryRequest
    {
        public int? ConversationId { get; set; }
        public int? ClientId { get; set; }
        public string Question { get; set; }
    }
}