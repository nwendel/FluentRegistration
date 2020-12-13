using System;
using FluentRegistration.Infrastructure;

namespace FluentRegistration.Internal
{
    public interface IComponentImplementationSelector<TService> :
        IFluentInterface
    {
        #region Implemented By

        ILifetime ImplementedBy<TImplementation>()
            where TImplementation : TService;

        #endregion

        #region Instance

        IValidRegistration Instance(TService instance);

        #endregion

        #region Using Factory

        IValidRegistration UsingFactory<TFactory>(Func<TFactory, TService> factoryMethod)
            where TFactory : class;

        #endregion

        #region Using Factory Method

        IValidRegistration UsingFactoryMethod(Func<TService> factoryMethod);

        IValidRegistration UsingFactoryMethod(Func<IServiceProvider, TService> factoryMethod);

        #endregion
    }
}
