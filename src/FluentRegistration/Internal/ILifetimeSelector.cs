namespace FluentRegistration.Internal;

public interface ILifetimeSelector<T> : IFluentInterface
    where T : IHasServiceKeySelectorBase
{
    IHasServiceKey<T> Singleton();

    IHasServiceKey<T> Scoped();

    IHasServiceKey<T> Transient();
}
