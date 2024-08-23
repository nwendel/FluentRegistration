namespace FluentRegistration.Internal;

public class LifetimeAndKeySelector : ILifetimeSelector, IHasKey, IHasKeySelector, IValidRegistration
{
    private ServiceLifetime? _lifetime;

    public ServiceLifetime Lifetime => _lifetime ?? throw new InvalidOperationException("No lifetime defined");

    public Func<Type, Type, object>? Key { get; private set; }

    public IHasKey Singleton()
    {
        _lifetime = ServiceLifetime.Singleton;
        return this;
    }

    public IHasKey Scoped()
    {
        _lifetime = ServiceLifetime.Scoped;
        return this;
    }

    public IHasKey Transient()
    {
        _lifetime = ServiceLifetime.Transient;
        return this;
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1201:Elements should appear in the correct order", Justification = "Group Key methods together")]
    public IHasKeySelector HasKey => this;

    public IValidRegistration ImplementationType()
    {
        Key = (serviceType, implementationType) => implementationType.Name;
        return this;
    }
}
