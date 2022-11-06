using System;
using Microsoft.Extensions.DependencyInjection;

namespace FluentRegistration.Internal;

public class ComponentFactoryMethodRegistration<TService> :
    IValidRegistration,
    IRegister
    where TService : notnull
{
    private readonly Func<IServiceProvider, TService> _factoryMethod;

    public ComponentFactoryMethodRegistration(Func<IServiceProvider, TService> factoryMethod)
    {
        _factoryMethod = factoryMethod;
    }

    public void Register(IServiceCollection services)
    {
        var serviceDescriptor = new ServiceDescriptor(typeof(TService), serviceProvider => _factoryMethod(serviceProvider), ServiceLifetime.Transient);
        services.Add(serviceDescriptor);
    }
}
