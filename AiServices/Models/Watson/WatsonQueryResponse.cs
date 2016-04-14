using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiServices.Models.Watson
{
    public class WatsonQueryResponse
    {
        public WatsonConversationResponse ConversationResponse { get; set; }

        public WatsonProfileResponse ProfileResponse { get; set; }
    }
}
