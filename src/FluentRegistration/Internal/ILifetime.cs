namespace FluentRegistration.Internal;

public interface ILifetime<T>
    where T : IHasServiceKeySelectorBase
{
    ILifetimeSelector<T> Lifetime { get; }
}
