namespace FluentRegistration.Internal;

public interface ILifetime<T>
    where T : IHasKeySelectorBase
{
    ILifetimeSelector<T> Lifetime { get; }
}
