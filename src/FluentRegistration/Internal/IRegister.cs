using Microsoft.Extensions.DependencyInjection;

namespace FluentRegistration.Internal;

public interface IRegister
{
    void Register(IServiceCollection services);
}
