namespace FluentRegistration.Internal;

public class ComponentFactoryRegistration<TFactory, TService> : ILifetime, IRegister
    where TFactory : class
    where TService : notnull
{
    private readonly Func<TFactory, TService> _factoryMethod;
    private readonly LifetimeAndKeySelector _lifetimeAndKeySelector = new();

    public ComponentFactoryRegistration(Func<TFactory, TService> factoryMethod)
    {
        _factoryMethod = factoryMethod;
    }

    public ILifetimeSelector Lifetime => _lifetimeAndKeySelector;

    public void Register(IServiceCollection services)
    {
        var serviceDescriptor = new ServiceDescriptor(
            typeof(TService),
            _lifetimeAndKeySelector.Key,
            (serviceProvider, serviceKey) =>
            {
                var factory = serviceProvider.GetRequiredService<TFactory>();
                return _factoryMethod(factory);
            },
            _lifetimeAndKeySelector.Lifetime);
        services.Add(serviceDescriptor);
    }
}
