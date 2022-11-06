using Microsoft.Extensions.DependencyInjection;

namespace FluentRegistration.Internal;

public class LifetimeSelector :
    ILifetimeSelector,
    IValidRegistration
{
    public ServiceLifetime Lifetime { get; private set; } = ServiceLifetime.Singleton;

    public IValidRegistration Singleton()
    {
        Lifetime = ServiceLifetime.Singleton;
        return this;
    }

    public IValidRegistration Scoped()
    {
        Lifetime = ServiceLifetime.Scoped;
        return this;
    }

    public IValidRegistration Transient()
    {
        Lifetime = ServiceLifetime.Transient;
        return this;
    }
}
