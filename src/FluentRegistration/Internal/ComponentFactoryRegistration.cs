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
        // TODO: The GetRequiredService below how to deal with keys there?
        var serviceDescriptor = new ServiceDescriptor(
            typeof(TService),
            _lifetimeAndKeySelector.FactoryKey(typeof(TService)),
            (serviceProvider, serviceKey) =>
            {
                var factory = serviceProvider.GetRequiredService<TFactory>();
                return _factoryMethod(factory);
            },
            _lifetimeAndKeySelector.Lifetime);
        services.Add(serviceDescriptor);
    }
}
