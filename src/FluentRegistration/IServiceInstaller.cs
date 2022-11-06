using Microsoft.Extensions.DependencyInjection;

namespace FluentRegistration;

public interface IServiceInstaller
{
    void Install(IServiceCollection services);
}
