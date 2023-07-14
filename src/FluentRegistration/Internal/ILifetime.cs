namespace FluentRegistration.Internal;

public interface ILifetime
{
    ILifetimeSelector Lifetime { get; }
}
