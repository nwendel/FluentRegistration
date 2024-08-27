namespace FluentRegistration.Internal;

public interface IComponentImplementationSelector<TService> : IFluentInterface
{
    ILifetime<IHasServiceKeySelectorComponent> ImplementedBy<TImplementation>()
        where TImplementation : TService;

    IValidRegistration Instance(TService instance);

    ILifetime<IHasServiceKeySelectorFactory> UsingFactory<TFactory>(Func<TFactory, TService> factoryMethod)
        where TFactory : class;

    ILifetime<IHasServiceKeySelectorFactory> UsingFactoryMethod(Func<TService> factoryMethod);

    ILifetime<IHasServiceKeySelectorFactory> UsingFactoryMethod(Func<IServiceProvider, TService> factoryMethod);
}
