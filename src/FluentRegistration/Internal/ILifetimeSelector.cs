namespace FluentRegistration.Internal;

public interface ILifetimeSelector : IFluentInterface
{
    IValidRegistration Singleton();

    IValidRegistration Scoped();

    IValidRegistration Transient();
}
