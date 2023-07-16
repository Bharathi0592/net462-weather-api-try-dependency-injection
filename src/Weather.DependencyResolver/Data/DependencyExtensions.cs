using System;
using Weather.DependencyResolver.Data;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class DependencyExtensions
    {
        const string Sql = nameof(Sql);
        const string Oracle = nameof(Oracle);
        const string Redis = nameof(Redis);

        public static IServiceCollection AddWeatherQueryObjects(this IServiceCollection services, Action<DbOptions> config)
        {
            DbOptions options = new DbOptions();
            config.Invoke(options);
            services.AddSingleton(options);

            switch (options.ProviderName)
            {
                case Sql:
                    services.AddSqlWeatherForecasts();
                    break;
                case Redis:
                    services.AddRedisWeatherForecasts();
                    break;

                case Oracle:
                default:
                    throw new NotSupportedException(options.ProviderName);
            }

            return services;
        }

        public static IServiceCollection AddRedisWeatherForecasts(this IServiceCollection services)
            => services.AddSingleton<IWeatherForecastQueryObject, WeatherForecastRedisQueryObject>();

        public static IServiceCollection AddSqlWeatherForecasts(this IServiceCollection services)
            => services.AddSingleton<IWeatherForecastQueryObject, WeatherForecastSqlQueryObject>();

    }
}
