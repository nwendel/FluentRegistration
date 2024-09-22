namespace FluentRegistration.Internal;

public class ComponentFactoryRegistration<TFactory, TService> : ILifetime<IHasServiceKeySelectorFactory>, IRegister
    where TFactory : class
    where TService : notnull
{
    private readonly Func<TFactory, TService> _factoryMethod;
    private readonly LifetimeAndServiceKeySelector<IHasServiceKeySelectorFactory> _lifetimeAndServiceKeySelector = new();

    public ComponentFactoryRegistration(Func<TFactory, TService> factoryMethod)
    {
        _factoryMethod = factoryMethod;
    }

    public ILifetimeSelector<IHasServiceKeySelectorFactory> Lifetime => _lifetimeAndServiceKeySelector;

    public void Register(IServiceCollection services)
    {
        GuardAgainst.Null(services);

        var serviceKey = _lifetimeAndServiceKeySelector.GetFactoryServiceKey(typeof(TService));
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
            _lifetimeAndServiceKeySelector.Lifetime);
        services.Add(serviceDescriptor);
    }
}
