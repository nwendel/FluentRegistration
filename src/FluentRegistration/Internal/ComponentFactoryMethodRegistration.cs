namespace FluentRegistration.Internal;

public class ComponentFactoryMethodRegistration<TService> : ILifetime<IHasServiceKeySelectorFactory>, IRegister
    where TService : notnull
{
    private readonly Func<IServiceProvider, TService> _factoryMethod;
    private readonly LifetimeAndServiceKeySelector<IHasServiceKeySelectorFactory> _lifetimeAndServiceKeySelector = new();

    public ComponentFactoryMethodRegistration(Func<IServiceProvider, TService> factoryMethod)
    {
        _factoryMethod = factoryMethod;
    }

    public ILifetimeSelector<IHasServiceKeySelectorFactory> Lifetime => _lifetimeAndServiceKeySelector;

    public void Register(IServiceCollection services)
    {
        GuardAgainst.Null(services);

        var serviceDescriptor = new ServiceDescriptor(typeof(TService), _lifetimeAndServiceKeySelector.GetFactoryServiceKey(typeof(TService)), (serviceProvider, serviceKey) => _factoryMethod(serviceProvider), _lifetimeAndServiceKeySelector.Lifetime);
        services.Add(serviceDescriptor);
    }
}
