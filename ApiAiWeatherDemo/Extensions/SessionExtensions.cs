using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace ApiAiWeatherDemo.Extensions
{
    [Serializable]
    public class UserSettings
    {
        public string ApiAiSessionId { get; set; }
        public int WatsonClientId { get; set; }
        public int WatsonConversationId { get; set; }
    }
    public static class SessionExtensions
    {
        private const string UserSettings = "user";
        public static UserSettings GetUserSettings(this HttpSessionState session)
        {
            return session[UserSettings] as UserSettings;
        }
        public static void SaveUserSettings(this HttpSessionState session, UserSettings settings)
        {
            session[UserSettings] = settings;
        }
    }
}