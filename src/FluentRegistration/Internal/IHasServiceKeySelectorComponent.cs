namespace FluentRegistration.Internal;

// TODO: Service not Component?
public interface IHasServiceKeySelectorComponent : IHasServiceKeySelectorBase
{
    IValidRegistration ImplementationType();

    IValidRegistration FromImplementation();
}
