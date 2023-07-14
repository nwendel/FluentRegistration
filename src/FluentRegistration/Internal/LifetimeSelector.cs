namespace FluentRegistration.Internal;

public class LifetimeSelector : ILifetimeSelector, IValidRegistration
{
    private ServiceLifetime? _lifetime;

    public ServiceLifetime Lifetime => _lifetime ?? throw new InvalidOperationException("No lifetime defined");

    public IValidRegistration Singleton()
    {
        _lifetime = ServiceLifetime.Singleton;
        return this;
    }

    public IValidRegistration Scoped()
    {
        _lifetime = ServiceLifetime.Scoped;
        return this;
    }

    public IValidRegistration Transient()
    {
        _lifetime = ServiceLifetime.Transient;
        return this;
    }
}
