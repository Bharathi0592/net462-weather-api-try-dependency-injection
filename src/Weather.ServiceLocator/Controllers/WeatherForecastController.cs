using System.Collections.Generic;
using System.Web.Http;
using Weather.ServiceLocator.Business;
using Weather.ServiceLocator.Models;

namespace Weather.ServiceLocator.Controllers
{
    [RoutePrefix("api/weather-forecast")]
    public sealed class WeatherForecastController : ApiController
    {
        readonly WeatherForecastBusinessObject _weather;

        public WeatherForecastController()
            : this(WeatherServiceLocator.Instance.GetService<WeatherForecastBusinessObject>()) { }

        private WeatherForecastController(WeatherForecastBusinessObject weather)
        {
            _weather = weather;
        }

        // GET api/weather-forecast
        [Route("")]
        public IEnumerable<WeatherForecast> Get()
        {
            return _weather.GetLatestForecasts();
        }

        // GET api/weather-forecast/5
        [Route("{id}")]
        public WeatherForecast Get(int id)
        {
            return _weather.GetForecast(id);
        }

        //// POST api/weather-forecast
        //[Route("")]
        //public void Post([FromBody] WeatherForecast value)
        //{
        //}

        //// PUT api/weather-forecast/5
        //[Route("{id}")]
        //public void Put(int id, [FromBody] WeatherForecast value)
        //{
        //}

        //// DELETE api/weather-forecast/5
        //[Route("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
