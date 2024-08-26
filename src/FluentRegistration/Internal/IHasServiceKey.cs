namespace FluentRegistration.Internal;

public interface IHasServiceKey<T> : IValidRegistration, IFluentInterface
    where T : IHasServiceKeySelectorBase
{
    T HasServiceKey { get; }
}
