namespace FluentRegistration.Internal;

public interface ILifetimeSelector<T> : IFluentInterface
    where T : IHasKeySelectorBase
{
    IHasKey<T> Singleton();

    IHasKey<T> Scoped();

    IHasKey<T> Transient();
}
