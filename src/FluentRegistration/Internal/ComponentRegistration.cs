using System;
using FluentRegistration.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace FluentRegistration.Internal
{
    public class ComponentRegistration<TService> :
        IComponentImplementationSelector<TService>,
        IRegister
        where TService : class
    {
        #region Fields

        private readonly Type[] _serviceTypes;
        private IRegister? _register;

        #endregion

        #region Constructor

        public ComponentRegistration(Type[] serviceTypes)
        {
            _serviceTypes = serviceTypes;
        }

        #endregion

        #region Implemented By

        public ILifetime ImplementedBy<TImplementation>()
            where TImplementation : TService
        {
            var implementedByRegistration = new ComponentImplementedByRegistration<TService, TImplementation>(_serviceTypes);
            _register = implementedByRegistration;
            return implementedByRegistration;
        }

        #endregion

        #region Instance

        public IValidRegistration Instance(TService instance)
        {
            GuardAgainst.Null(instance);

            var instanceRegistration = new ComponentInstanceRegistration(_serviceTypes, instance);
            _register = instanceRegistration;
            return instanceRegistration;
        }

        #endregion

        #region Using Factory

        public IValidRegistration UsingFactory<TFactory>(Func<TFactory, TService> factoryMethod)
            where TFactory : class
        {
            GuardAgainst.Null(factoryMethod);

            var factoryRegistration = new ComponentFactoryRegistration<TFactory, TService>(factoryMethod);
            _register = factoryRegistration;
            return factoryRegistration;
        }

        #endregion

        #region Using Factory Method

        public IValidRegistration UsingFactoryMethod(Func<TService> factoryMethod)
        {
            GuardAgainst.Null(factoryMethod);

            return UsingFactoryMethod(serviceProvider => factoryMethod());
        }

        public IValidRegistration UsingFactoryMethod(Func<IServiceProvider, TService> factoryMethod)
        {
            GuardAgainst.Null(factoryMethod);

            var factoryMethodRegistration = new ComponentFactoryMethodRegistration<TService>(factoryMethod);
            _register = factoryMethodRegistration;
            return factoryMethodRegistration;
        }

        #endregion

        #region Register

        public void Register(IServiceCollection services)
        {
            if (_register == null)
            {
                throw new InvalidOperationException("Register called without defining what to register via the fluent Api.");
            }

            _register.Register(services);
        }

        #endregion
    }
}
