namespace FluentRegistration.Internal;

public interface ILifetime :
    IValidRegistration
{
    ILifetimeSelector Lifetime { get; }
}
