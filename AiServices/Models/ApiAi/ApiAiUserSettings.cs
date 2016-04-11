using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiServices.Models.ApiAi
{
    public class ApiAiUserSettings
    {
        public string SessionId { get; private set; }

        public ApiAiUserSettings(string sessionId)
        {
            SessionId = sessionId;
        }
    }
}
