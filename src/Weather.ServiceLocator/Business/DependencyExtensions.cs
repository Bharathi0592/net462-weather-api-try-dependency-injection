using Weather.ServiceLocator.Business;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class DependencyExtensions
    {
        public static IServiceCollection AddWeatherBusinessObjects(this IServiceCollection services)
        {
            services.AddTransient<WeatherForecastBusinessObject>();

            return services;
        }
    }
}
