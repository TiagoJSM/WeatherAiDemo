using AiServices;
using AiServices.Models.ApiAi;
using AiServices.Services.WeatherServices;
using ApiAiWeatherDemo.App_Start;
using ApiAiWeatherDemo.Extensions;
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
                .RegisterType<UserSettings>(
                    new InjectionFactory(c =>
                    {
                        var settings = HttpContext.Current.Session.GetUserSettings();
                        if (settings == null)
                        {
                            settings = new UserSettings();
                        }
                        if (settings.ApiAiSessionId == null)
                        {
                            settings.ApiAiSessionId = Guid.NewGuid().ToString().Substring(0, 36);   //36 is defined by API.AI
                            HttpContext.Current.Session.SaveUserSettings(settings);
                        }
                        return settings;
                    }))
                .RegisterType<ApiAiUserSettings>(
                    new InjectionFactory(c =>
                    {
                        var settings = c.Resolve<UserSettings>();
                        return new ApiAiUserSettings(settings.ApiAiSessionId);
                    }));

            config.DependencyResolver = new UnityResolver(container);
        }
    }
}
