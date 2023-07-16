using System.Configuration;
using System.Web;
using System.Web.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Weather.ServiceLocator
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(config =>
            {
                // HINT: Dependency injection
                WeatherServiceLocator.Instance.ConfigureServices(services =>
                {
                    services.AddWeatherQueryObjects(opt =>
                    {
                        opt.ProviderName = ConfigurationManager.AppSettings["WeatherForecast.Provider"];
                        opt.ConnectionString = ConfigurationManager.ConnectionStrings[$"WeatherForecast.{opt.ProviderName}"]
                                                                   .ConnectionString;
                    });
                    services.AddWeatherBusinessObjects();
                });

                // Web API routes
                config.MapHttpAttributeRoutes();
            });
        }
    }
}
