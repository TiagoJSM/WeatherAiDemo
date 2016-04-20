using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiServices.Models.Slack
{
    public class SlackOutgoingData
    {
        public String token { get; set; }
        public String team_id { get; set; }
        public String channel_id { get; set; }
        public String channel_name { get; set; }
        public long timestamp { get; set; }
        public String user_id { get; set; }
        public String user_name { get; set; }
        public String text { get; set; }
        public String trigger_word { get; set; }
    }
}
