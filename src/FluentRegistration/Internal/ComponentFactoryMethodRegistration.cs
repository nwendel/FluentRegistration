namespace FluentRegistration.Internal;

public class ComponentFactoryMethodRegistration<TService> : ILifetime, IRegister
    where TService : notnull
{
    private readonly Func<IServiceProvider, TService> _factoryMethod;
    private readonly LifetimeAndKeySelector _lifetimeAndKeySelector = new();

    public ComponentFactoryMethodRegistration(Func<IServiceProvider, TService> factoryMethod)
    {
        _factoryMethod = factoryMethod;
    }

    public ILifetimeSelector Lifetime => _lifetimeAndKeySelector;

    public void Register(IServiceCollection services)
    {
        var serviceDescriptor = new ServiceDescriptor(typeof(TService), _lifetimeAndKeySelector.Key, (serviceProvider, serviceKey) => _factoryMethod(serviceProvider), _lifetimeAndKeySelector.Lifetime);
        services.Add(serviceDescriptor);
    }
}
