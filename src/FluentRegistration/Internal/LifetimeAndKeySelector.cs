namespace FluentRegistration.Internal;

// TODO: Can I split this in two classes somehow?
public class LifetimeAndKeySelector<T> :
    ILifetimeSelector<T>,
    IHasKey<T>,
    IHasKeySelectorFactory,
    IHasKeySelectorComponent,
    IValidRegistration
    where T : IHasKeySelectorBase
{
    private ServiceLifetime? _lifetime;
    private bool _hasKey;
    private Func<Type, object>? _serviceKeySelector;
    private Func<Type, Type, object>? _implementationKeySelector;

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
    public T HasKey
    {
        get
        {
            _hasKey = true;
            return (T)(IHasKeySelectorBase)this;
        }
    }

    public IValidRegistration Value(object key)
    {
        GuardAgainst.Null(key);

        _serviceKeySelector = serviceType => key;
        return this;
    }

    public IValidRegistration ImplementationType()
    {
        _implementationKeySelector = (serviceType, implementationType) => implementationType;
        return this;
    }

    public object? FactoryKey(Type serviceType)
    {
        GuardAgainst.Null(serviceType);

        if (!_hasKey)
        {
            return null;
        }

        if (_serviceKeySelector == null)
        {
            throw new InvalidOperationException("HasKey without KeySelector");
        }

        var key = _serviceKeySelector.Invoke(serviceType);
        return key;
    }

    public object? ComponentKey(Type serviceType, Type implementationType)
    {
        GuardAgainst.Null(serviceType);
        GuardAgainst.Null(implementationType);

        if (!_hasKey)
        {
            return null;
        }

        if (_implementationKeySelector != null)
        {
            return _implementationKeySelector.Invoke(serviceType, implementationType);
        }

        return FactoryKey(serviceType);
    }
}
