namespace FluentRegistration.Internal;

public class ComponentFactoryRegistration<TFactory, TService> : ILifetime, IRegister
    where TFactory : class
    where TService : notnull
{
    private readonly Func<TFactory, TService> _factoryMethod;
    private readonly LifetimeSelector _lifetimeSelector = new();

    public ComponentFactoryRegistration(Func<TFactory, TService> factoryMethod)
    {
        _factoryMethod = factoryMethod;
    }

    public ILifetimeSelector Lifetime => _lifetimeSelector;

    public void Register(IServiceCollection services)
    {
        var serviceDescriptor = new ServiceDescriptor(
            typeof(TService),
            serviceProvider =>
            {
                var factory = serviceProvider.GetRequiredService<TFactory>();
                return _factoryMethod(factory);
            },
            _lifetimeSelector.Lifetime);
        services.Add(serviceDescriptor);
    }
}
