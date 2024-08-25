namespace FluentRegistration.Internal;

public interface IComponentImplementationSelector<TService> : IFluentInterface
{
    ILifetime<IHasKeySelectorComponent> ImplementedBy<TImplementation>()
        where TImplementation : TService;

    IValidRegistration Instance(TService instance);

    ILifetime<IHasKeySelectorFactory> UsingFactory<TFactory>(Func<TFactory, TService> factoryMethod)
        where TFactory : class;

    ILifetime<IHasKeySelectorFactory> UsingFactoryMethod(Func<TService> factoryMethod);

    ILifetime<IHasKeySelectorFactory> UsingFactoryMethod(Func<IServiceProvider, TService> factoryMethod);
}
