namespace FluentRegistration.Internal;

public interface IComponentImplementationSelector<TService> : IFluentInterface
{
    ILifetime ImplementedBy<TImplementation>()
        where TImplementation : TService;

    IValidRegistration Instance(TService instance);

    ILifetime UsingFactory<TFactory>(Func<TFactory, TService> factoryMethod)
        where TFactory : class;

    ILifetime UsingFactoryMethod(Func<TService> factoryMethod);

    ILifetime UsingFactoryMethod(Func<IServiceProvider, TService> factoryMethod);
}
