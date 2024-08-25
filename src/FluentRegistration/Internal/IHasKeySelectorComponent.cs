namespace FluentRegistration.Internal;

public interface IHasKeySelectorComponent : IHasKeySelectorBase
{
    IValidRegistration ImplementationType();
}
