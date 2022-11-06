using FluentRegistration.Infrastructure;

namespace FluentRegistration.Internal;

public interface IWithServicesInitial :
    IFluentInterface
{
    IServiceSelector WithServices { get; }
}
