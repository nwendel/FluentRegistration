using FluentRegistration.Infrastructure;

namespace FluentRegistration.Internal;

public interface IServiceSelector :
    IFluentInterface
{
    IWithServices AllInterfaces();

    IWithServices DefaultInterface();

    IWithServices Service<TService>();

    IWithServices Self();
}
