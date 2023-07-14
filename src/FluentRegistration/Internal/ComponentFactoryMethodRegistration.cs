namespace FluentRegistration.Internal;

public class ComponentFactoryMethodRegistration<TService> : ILifetime, IRegister
    where TService : notnull
{
    private readonly Func<IServiceProvider, TService> _factoryMethod;
    private readonly LifetimeSelector _lifetimeSelector = new();

    public ComponentFactoryMethodRegistration(Func<IServiceProvider, TService> factoryMethod)
    {
        _factoryMethod = factoryMethod;
    }

    public ILifetimeSelector Lifetime => _lifetimeSelector;

    public void Register(IServiceCollection services)
    {
        var serviceDescriptor = new ServiceDescriptor(typeof(TService), serviceProvider => _factoryMethod(serviceProvider), _lifetimeSelector.Lifetime);
        services.Add(serviceDescriptor);
    }
}
