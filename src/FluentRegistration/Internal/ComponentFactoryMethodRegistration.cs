namespace FluentRegistration.Internal;

public class ComponentFactoryMethodRegistration<TService> : ILifetime<IHasKeySelectorFactory>, IRegister
    where TService : notnull
{
    private readonly Func<IServiceProvider, TService> _factoryMethod;
    private readonly LifetimeAndKeySelector<IHasKeySelectorFactory> _lifetimeAndKeySelector = new();

    public ComponentFactoryMethodRegistration(Func<IServiceProvider, TService> factoryMethod)
    {
        _factoryMethod = factoryMethod;
    }

    public ILifetimeSelector<IHasKeySelectorFactory> Lifetime => _lifetimeAndKeySelector;

    public void Register(IServiceCollection services)
    {
        var serviceDescriptor = new ServiceDescriptor(typeof(TService), _lifetimeAndKeySelector.FactoryKey(typeof(TService)), (serviceProvider, serviceKey) => _factoryMethod(serviceProvider), _lifetimeAndKeySelector.Lifetime);
        services.Add(serviceDescriptor);
    }
}
