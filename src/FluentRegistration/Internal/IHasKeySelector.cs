namespace FluentRegistration.Internal;

public interface IHasKeySelector : IValidRegistration
{
    IValidRegistration ImplementationType();
}
