using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ApiAiWeatherDemo.Startup))]
namespace ApiAiWeatherDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
