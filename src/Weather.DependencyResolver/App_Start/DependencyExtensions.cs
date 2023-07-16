using System;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Dependencies;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class DependencyExtensions
    {
        public static HttpConfiguration MapEndpoints(this HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            return config;
        }

        public static IServiceCollection ConfigureServices(this HttpConfiguration config, Action<IServiceCollection> configureServices)
        {
            IServiceCollection services = new ServiceCollection();
            configureServices.Invoke(services);

            services.AddControllers();

            return services;
        }

        public static IServiceCollection AddControllers(this IServiceCollection services)
            => services.AddControllersFromAssembly(Assembly.GetExecutingAssembly());

        public static IServiceCollection AddControllersFromAssembly(this IServiceCollection services, Assembly assemblyMarker)
        {
            assemblyMarker.GetTypes()
                          // HINT: Filter WebApi Controllers.
                          //       To filter Mvc Controllers, use typeof(Controller).
                          .Where(t => typeof(ApiController).IsAssignableFrom(t))
                          .ToList()

                          // HINT: Register
                          .ForEach(controller => services.AddTransient(controller));

            return services;
        }

        public static IDependencyResolver CreateNetCoreDependencyResolver(this IServiceProvider serviceCtor)
            => new NetCoreDependencyResolver(serviceCtor);

        public static HttpConfiguration UseNetCoreDependencyResolver(this IServiceProvider serviceCtor, HttpConfiguration config)
        {
            config.DependencyResolver = serviceCtor.CreateNetCoreDependencyResolver();

            return config;
        }
    }
}
