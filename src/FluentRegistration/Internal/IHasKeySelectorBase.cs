namespace FluentRegistration.Internal;

public interface IHasKeySelectorBase : IFluentInterface
{
    IValidRegistration Value(object key);
}
