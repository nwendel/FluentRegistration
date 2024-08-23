namespace FluentRegistration.Internal;

public interface ILifetimeSelector : IFluentInterface
{
    IHasKey Singleton();

    IHasKey Scoped();

    IHasKey Transient();
}
