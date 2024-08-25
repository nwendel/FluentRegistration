namespace FluentRegistration.Internal;

public interface IHasKey<T> : IValidRegistration, IFluentInterface
    where T : IHasKeySelectorBase
{
    T HasKey { get; }
}
