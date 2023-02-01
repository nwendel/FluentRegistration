# FluentRegistration ![Build](https://github.com/nwendel/fluentregistration/actions/workflows/build.yml/badge.svg) [![Coverage](https://codecov.io/gh/nwendel/fluentregistration/branch/main/graph/badge.svg?token=BMNOSIWUMV)](https://codecov.io/gh/nwendel/fluentregistration)

A fluent API for registering services with Microsoft.Extensions.DependencyInjection

### Example
```csharp
public class Example
{

    public void ConfigureServices(IServiceCollection services)
    {
        services.Register(r => r
            .FromThisAssembly()
            .Where(c => c.InThisNamespace() && c.AssignableTo<IService>())
            .WithServices
                .AllInterfaces()
                .Self()
            .Lifetime.Singleton());
            
        services.Register(c => c
            .For<IService>()
            .ImplementedBy<Implementation>()
            .Lifetime.Singleton());
            
        services.Register(r => r
            .ImplementedBy<Implementation>()
            .WithServices.AllInterfaces()
            .Lifetime.Singleton());
            
        services.Install<ServiceInstaller>();
        
        services.Install(i => i.FromThisAssembly());
    }

}

public class ServiceInstaller : IServiceInstaller
{

    public void Install(IServiceCollection services)
    {
        // Register services here
    }

}
```
