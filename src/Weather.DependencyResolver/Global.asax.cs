using System.Configuration;
using System.Web;
using System.Web.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Weather.DependencyResolver
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(config =>
            {
                config.ConfigureServices(services =>
                      {
                          services.AddWeatherQueryObjects(opt =>
                          {
                              opt.ProviderName = ConfigurationManager.AppSettings["WeatherForecast.Provider"];
                              opt.ConnectionString = ConfigurationManager.ConnectionStrings[$"WeatherForecast.{opt.ProviderName}"]
                                                                         .ConnectionString;
                          });
                          services.AddWeatherBusinessObjects();
                      })
                      .BuildServiceProvider()
                      .UseNetCoreDependencyResolver(config)
                      // Web API routes
                      .MapEndpoints()
                      .EnsureInitialized();
            });
        }
    }
}
