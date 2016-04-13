using AiServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AiServices.Models.Luis;
using ApiAiWeatherDemo.Extensions;

namespace ApiAiWeatherDemo.SessionManagement
{
    public class LuisContextSession : ILuisContextSession
    {
        private UserSettings _settings;

        public LuisContextSession(UserSettings settings)
        {
            _settings = settings;
        }

        public LuisContext GetContext()
        {
            return new LuisContext()
            {
                Location = _settings.LuisLocationContext
            };
        }

        public void SaveContext(LuisContext context)
        {
            _settings.LuisLocationContext = context.Location;
            HttpContext.Current.Session.SaveUserSettings(_settings);
        }
    }
}