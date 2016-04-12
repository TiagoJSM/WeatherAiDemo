using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiServices.Models.Watson
{
    public class WatsonUserSettings
    {
        public int ClientId { get; set; }
        public int ConversationId { get; set; }

        public WatsonUserSettings(int clientId, int conversationId)
        {
            ClientId = clientId;
            ConversationId = conversationId;
        }
    }
}
