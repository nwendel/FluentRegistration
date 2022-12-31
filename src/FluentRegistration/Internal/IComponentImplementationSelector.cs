using FluentRegistration.Infrastructure;

namespace FluentRegistration.Internal;

public interface IComponentImplementationSelector<TService> :
    IFluentInterface
{
    ILifetime ImplementedBy<TImplementation>()
        where TImplementation : TService;

    IValidRegistration Instance(TService instance);

    IValidRegistration UsingFactory<TFactory>(Func<TFactory, TService> factoryMethod)
        where TFactory : class;

    IValidRegistration UsingFactoryMethod(Func<TService> factoryMethod);

    IValidRegistration UsingFactoryMethod(Func<IServiceProvider, TService> factoryMethod);
}
