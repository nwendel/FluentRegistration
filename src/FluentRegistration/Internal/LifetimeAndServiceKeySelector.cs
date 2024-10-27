using System.Reflection;

namespace FluentRegistration.Internal;

// TODO: Can I split this in two classes somehow?
public class LifetimeAndServiceKeySelector<T> :
    ILifetimeSelector<T>,
    IHasServiceKey<T>,
    IHasServiceKeySelectorFactory,
    IHasServiceKeySelectorComponent,
    IValidRegistration
    where T : IHasServiceKeySelectorBase
{
    private ServiceLifetime? _lifetime;
    private bool _hasServiceKey;
    private Func<Type, object>? _serviceKeySelector;
    private Func<Type, Type, object>? _implementationKeySelector;

    public ServiceLifetime Lifetime => _lifetime ?? throw new InvalidOperationException("No lifetime defined");

    public IHasServiceKey<T> Singleton()
    {
        _lifetime = ServiceLifetime.Singleton;
        return this;
    }

    public IHasServiceKey<T> Scoped()
    {
        _lifetime = ServiceLifetime.Scoped;
        return this;
    }

    public IHasServiceKey<T> Transient()
    {
        _lifetime = ServiceLifetime.Transient;
        return this;
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1201:Elements should appear in the correct order", Justification = "Group ServiceKey methods together")]
    public T HasServiceKey
    {
        get
        {
            _hasServiceKey = true;
            return (T)(IHasServiceKeySelectorBase)this;
        }
    }

    public IValidRegistration Value(object serviceKey)
    {
        GuardAgainst.Null(serviceKey);

        _serviceKeySelector = serviceType => serviceKey;
        return this;
    }

    public IValidRegistration ImplementationType()
    {
        _implementationKeySelector = (serviceType, implementationType) => implementationType;
        return this;
    }

    public IValidRegistration FromImplementation()
    {
        _implementationKeySelector = (serviceType, implementationType) =>
        {
            if (!typeof(IServiceKeyAware).IsAssignableFrom(implementationType))
            {
                throw new RegistrationException($"Implementation type {implementationType.FullName} does not implement {nameof(IServiceKeyAware)}");
            }

            var genericMethod = GetType()
                .GetMethods(BindingFlags.NonPublic | BindingFlags.Static)
                .Single(x => x.Name == nameof(FromHasServiceKey));
            var method = genericMethod.MakeGenericMethod(implementationType);
            var serviceKey = method.Invoke(null, null) ?? throw new RegistrationException($"ServiceKey for {implementationType.FullName} cannot be null");
            return serviceKey;
        };
        return this;
    }

    public object? GetFactoryServiceKey(Type serviceType)
    {
        GuardAgainst.Null(serviceType);

        if (!_hasServiceKey)
        {
            return null;
        }

        if (_serviceKeySelector == null)
        {
            throw new InvalidOperationException("Cannot get ServiceKey without KeySelector");
        }

        var serviceKey = _serviceKeySelector.Invoke(serviceType);
        return serviceKey;
    }

    public object? GetComponentServiceKey(Type serviceType, Type implementationType)
    {
        GuardAgainst.Null(serviceType);
        GuardAgainst.Null(implementationType);

        if (!_hasServiceKey)
        {
            return null;
        }

        if (_implementationKeySelector != null)
        {
            return _implementationKeySelector.Invoke(serviceType, implementationType);
        }

        return GetFactoryServiceKey(serviceType);
    }

    private static object FromHasServiceKey<TServiceKey>()
        where TServiceKey : IServiceKeyAware
        => TServiceKey.ServiceKey;
}
