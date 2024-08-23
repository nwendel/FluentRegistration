namespace FluentRegistration.Internal;

public interface IHasKey : IValidRegistration, IFluentInterface
{
    IHasKeySelector HasKey { get; }
}
