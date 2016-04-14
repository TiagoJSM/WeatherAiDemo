using AiServices;
using AiServices.Models.ApiAi;
using AiServices.Models.Watson;
using AiServices.Services.AiServices;
using AiServices.Services.WeatherServices;
using ApiAiWeatherDemo.App_Start;
using ApiAiWeatherDemo.Extensions;
using ApiAiWeatherDemo.SessionManagement;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ApiAiWeatherDemo
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );

            var container = new UnityContainer();
            container
                .RegisterType<IApiAiWeatherService, ApiAiWeatherService>()
                .RegisterType<ILuisWeatherService, LuisWeatherService>()
                .RegisterType<IWatsonWeatherService, WatsonWeatherService>()
                .RegisterType<IWatsonLoginService, WatsonLoginService>()
                .RegisterType<ILuisContextSession, LuisContextSession>()
                .RegisterType<UserSettings>(
                    new InjectionFactory(c =>
                    {
                        var settings = HttpContext.Current.Session.GetUserSettings();
                        if (settings == null)
                        {
                            settings = new UserSettings();
                            settings.ApiAiSessionId = Guid.NewGuid().ToString().Substring(0, 36);   //36 is defined by API.AI
                            var watsonSettings = c.Resolve<IWatsonLoginService>().GetUserSettings();
                            if (watsonSettings == null)
                            {
                                throw new NullReferenceException();
                            }
                            settings.WatsonClientId = watsonSettings.ClientId;
                            settings.WatsonConversationId = watsonSettings.ConversationId;
                            HttpContext.Current.Session.SaveUserSettings(settings);
                        }

                        return settings;
                    }))
                .RegisterType<ApiAiUserSettings>(
                    new InjectionFactory(c =>
                    {
                        var settings = c.Resolve<UserSettings>();
                        return new ApiAiUserSettings(settings.ApiAiSessionId);
                    }))
                .RegisterType<WatsonUserSettings>(
                    new InjectionFactory(c =>
                    {
                        var settings = c.Resolve<UserSettings>();
                        return new WatsonUserSettings(settings.WatsonClientId, settings.WatsonConversationId);
                    }))
                .RegisterType<WatsonData>(
                    new InjectionFactory(c =>
                    {
                        return new WatsonData()
                        {
                            Username = "200717c0-b5d8-4d36-8409-ff6eeef50a9a",
                            Password = "hoevZzE8cedp",
                            DialogueId = "e773d348-ed79-4558-b6d7-dfe119ecdfca"
                        };
                    }));

            config.DependencyResolver = new UnityResolver(container);
        }
    }
}
