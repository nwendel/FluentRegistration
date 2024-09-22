namespace FluentRegistration.Internal;

public interface IHasServiceKeySelectorBase : IFluentInterface
{
    IValidRegistration Value(object serviceKey);
}
