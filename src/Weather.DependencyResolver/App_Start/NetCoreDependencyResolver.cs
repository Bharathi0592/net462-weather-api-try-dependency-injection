using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;

namespace Microsoft.Extensions.DependencyInjection
{
    internal sealed class NetCoreDependencyResolver : IDependencyResolver
    {
        readonly IServiceProvider _serviceCtor;

        public NetCoreDependencyResolver(IServiceProvider serviceCtor)
        {
            _serviceCtor = serviceCtor;
        }

        public IDependencyScope BeginScope()
            => _serviceCtor.CreateScope().ServiceProvider.CreateNetCoreDependencyResolver();

        public object GetService(Type serviceType)
            => _serviceCtor.GetService(serviceType);

        public IEnumerable<object> GetServices(Type serviceType)
            => _serviceCtor.GetServices(serviceType);

        public void Dispose()
        {
            // HINT: Noop.
        }
    }
}
