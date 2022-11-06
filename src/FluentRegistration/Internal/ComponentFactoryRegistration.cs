using System;
using Microsoft.Extensions.DependencyInjection;

namespace FluentRegistration.Internal;

public class ComponentFactoryRegistration<TFactory, TService> :
    IValidRegistration,
    IRegister
    where TFactory : class
    where TService : notnull
{
    #region Fields

    private readonly Func<TFactory, TService> _factoryMethod;

    #endregion

    #region Constructor

    public ComponentFactoryRegistration(Func<TFactory, TService> factoryMethod)
    {
        _factoryMethod = factoryMethod;
    }

    #endregion

    #region Register

    public void Register(IServiceCollection services)
    {
        // TODO: Is this correct to add TFactory here as a singleton?
        // TODO: Should lifetime always be controlled by factory?
        services.AddSingleton<TFactory, TFactory>();

        var serviceDescriptor = new ServiceDescriptor(
            typeof(TService),
            serviceProvider =>
            {
                var factory = serviceProvider.GetRequiredService<TFactory>();
                return _factoryMethod(factory);
            },
            ServiceLifetime.Transient);
        services.Add(serviceDescriptor);
    }

    #endregion
}
