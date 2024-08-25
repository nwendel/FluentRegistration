namespace FluentRegistration.Internal;

public interface IWithServices : IServiceSelector, ILifetime<IHasKeySelectorComponent>
{
}
