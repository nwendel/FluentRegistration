# FluentRegistration [![Build status](https://ci.appveyor.com/api/projects/status/6o3s84ls1k92ktwp?svg=true)](https://ci.appveyor.com/project/nwendel/fluentregistration-p176b) [![Coverage Status](https://coveralls.io/repos/github/nwendel/FluentRegistration/badge.svg?branch=master)](https://coveralls.io/github/nwendel/FluentRegistration?branch=master)

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
            .WithServies
                .AllInterfaces()
                .Self());
    }

}
```
