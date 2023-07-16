using System.Linq;
using Weather.DependencyResolver.Data;
using Weather.DependencyResolver.Models;

namespace Weather.DependencyResolver.Business
{
    public sealed class WeatherForecastBusinessObject
    {
        readonly IWeatherForecastQueryObject _queryable;

        public WeatherForecastBusinessObject(IWeatherForecastQueryObject queryable)
        {
            _queryable = queryable;
        }

        public WeatherForecast[] GetLatestForecasts()
            => _queryable.GetTop(5);

        public WeatherForecast GetForecast(int id)
            => _queryable.GetTop(id).ElementAt(id - 1);
    }
}
