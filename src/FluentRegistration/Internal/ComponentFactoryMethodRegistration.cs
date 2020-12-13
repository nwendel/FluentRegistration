using System;
using Microsoft.Extensions.DependencyInjection;

namespace FluentRegistration.Internal
{
    public class ComponentFactoryMethodRegistration<TService> :
        IValidRegistration,
        IRegister
        where TService : notnull
    {
        #region Fields

        private readonly Func<IServiceProvider, TService> _factoryMethod;

        #endregion

        #region Constructor

        public ComponentFactoryMethodRegistration(Func<IServiceProvider, TService> factoryMethod)
        {
            _factoryMethod = factoryMethod;
        }

        #endregion

        #region Register

        public void Register(IServiceCollection services)
        {
            var serviceDescriptor = new ServiceDescriptor(typeof(TService), serviceProvider => _factoryMethod(serviceProvider), ServiceLifetime.Transient);
            services.Add(serviceDescriptor);
        }

        #endregion
    }
}
