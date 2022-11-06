using Microsoft.Extensions.DependencyInjection;

namespace FluentRegistration.Tests.Classes;

public class SimpleServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services)
    {
        services.Register(c => c
            .For<ISimpleService>()
            .ImplementedBy<SimpleService>());
    }
}
