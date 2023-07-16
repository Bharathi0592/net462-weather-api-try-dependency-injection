using System;
using Microsoft.Extensions.DependencyInjection;

namespace Weather.ServiceLocator
{
    public sealed class WeatherServiceLocator
    {
        static WeatherServiceLocator _instance;
        IServiceProvider _serviceCtor;

        WeatherServiceLocator() { }

        public static WeatherServiceLocator Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new WeatherServiceLocator();
                }

                return _instance;
            }
        }

        public void ConfigureServices(Action<IServiceCollection> factory)
        {
            IServiceCollection services = new ServiceCollection();
            factory.Invoke(services);

            _serviceCtor = services.BuildServiceProvider();
        }

        public TService GetService<TService>()
            => _serviceCtor.GetService<TService>();

        public TService GetRequiredService<TService>()
            => _serviceCtor.GetRequiredService<TService>();
    }
}
