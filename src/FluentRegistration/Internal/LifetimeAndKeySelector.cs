namespace FluentRegistration.Internal;

public class LifetimeAndKeySelector<T> :
    ILifetimeSelector<T>,
    IHasKey<T>,
    IHasKeySelectorFactory,
    IHasKeySelectorComponent,
    IValidRegistration
    where T : IHasKeySelectorBase
{
    private ServiceLifetime? _lifetime;
    private Func<Type, Type, object>? _keySelector;

    public ServiceLifetime Lifetime => _lifetime ?? throw new InvalidOperationException("No lifetime defined");

    public IHasKey<T> Singleton()
    {
        _lifetime = ServiceLifetime.Singleton;
        return this;
    }

    public IHasKey<T> Scoped()
    {
        _lifetime = ServiceLifetime.Scoped;
        return this;
    }

    public IHasKey<T> Transient()
    {
        _lifetime = ServiceLifetime.Transient;
        return this;
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1201:Elements should appear in the correct order", Justification = "Group Key methods together")]
    public T HasKey => (T)(IHasKeySelectorBase)this;

    public IValidRegistration Value(object key)
    {
        _keySelector = (serviceType, implementationType) => key;
        return this;
    }

    public IValidRegistration ImplementationType()
    {
        _keySelector = (serviceType, implementationType) => implementationType;
        return this;
    }

    public object? FactoryKey(Type serviceType)
    {
        GuardAgainst.Null(serviceType);

        var key = _keySelector?.Invoke(serviceType, null!);
        return key;
    }

    public object? ComponentKey(Type serviceType, Type implementationType)
    {
        GuardAgainst.Null(serviceType);
        GuardAgainst.Null(implementationType);

        var key = _keySelector?.Invoke(serviceType, implementationType);
        return key;
    }
}
