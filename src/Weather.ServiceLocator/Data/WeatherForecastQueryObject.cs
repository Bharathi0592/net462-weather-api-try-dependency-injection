using System;
using System.Linq;
using Weather.ServiceLocator.Models;

namespace Weather.ServiceLocator.Data
{
    public sealed class DbOptions
    {
        public string ProviderName { get; set; }
        public string ConnectionString { get; set; }
    }

    public interface IWeatherForecastQueryObject
    {
        WeatherForecast[] GetTop(int count);
    }

    internal sealed class WeatherForecastSqlQueryObject : IWeatherForecastQueryObject
    {
        static readonly string[] Summaries = new string[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        readonly DbOptions _options;

        public WeatherForecastSqlQueryObject(DbOptions options)
        {
            _options = options;
        }

        public WeatherForecast[] GetTop(int count)
        {
            return Enumerable.Range(1, count).Select(index => new WeatherForecast
            {
                Date = DateTimeOffset.UtcNow.AddDays(index),
                TemperatureC = new Random().Next(-20, 55),
                Summary = Summaries[new Random().Next(Summaries.Length)]
            })
            .ToArray();
        }
    }

    internal sealed class WeatherForecastRedisQueryObject : IWeatherForecastQueryObject
    {
        static readonly string[] Summaries = new string[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        readonly DbOptions _options;

        public WeatherForecastRedisQueryObject(DbOptions options)
        {
            _options = options;
        }

        public WeatherForecast[] GetTop(int count)
        {
            return Enumerable.Range(1, count).Select(index => new WeatherForecast
            {
                Date = DateTimeOffset.UtcNow.AddDays(index),
                TemperatureC = new Random().Next(-20, 55),
                Summary = Summaries[new Random().Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
