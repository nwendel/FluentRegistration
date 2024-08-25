namespace FluentRegistration.Internal;

public class ComponentFactoryRegistration<TFactory, TService> : ILifetime<IHasKeySelectorFactory>, IRegister
    where TFactory : class
    where TService : notnull
{
    private readonly Func<TFactory, TService> _factoryMethod;
    private readonly LifetimeAndKeySelector<IHasKeySelectorFactory> _lifetimeAndKeySelector = new();

    public ComponentFactoryRegistration(Func<TFactory, TService> factoryMethod)
    {
        _factoryMethod = factoryMethod;
    }

    public ILifetimeSelector<IHasKeySelectorFactory> Lifetime => _lifetimeAndKeySelector;

    public void Register(IServiceCollection services)
    {
        var serviceKey = _lifetimeAndKeySelector.FactoryKey(typeof(TService));
        var serviceDescriptor = new ServiceDescriptor(
            typeof(TService),
            serviceKey,
            (serviceProvider, serviceKey) =>
            {
                // TODO: Assumption is that the factory is also registered under the same key
                //       Is this correct?
                var factory = serviceProvider.GetRequiredKeyedService<TFactory>(serviceKey);
                return _factoryMethod(factory);
            },
            _lifetimeAndKeySelector.Lifetime);
        services.Add(serviceDescriptor);
    }
}
