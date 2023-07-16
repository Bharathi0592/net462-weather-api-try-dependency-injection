using System.Linq;
using Weather.ServiceLocator.Data;
using Weather.ServiceLocator.Models;

namespace Weather.ServiceLocator.Business
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
