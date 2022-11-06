using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace FluentRegistration.Internal;

public class ComponentInstanceRegistration :
    IValidRegistration,
    IRegister
{
    #region Fields

    private readonly IEnumerable<Type> _serviceTypes;
    private readonly object _instance;

    #endregion

    #region Constructor

    public ComponentInstanceRegistration(IEnumerable<Type> serviceTypes, object instance)
    {
        _serviceTypes = serviceTypes;
        _instance = instance;
    }

    #endregion

    #region Register

    public void Register(IServiceCollection services)
    {
        foreach (var serviceType in _serviceTypes)
        {
            var serviceDescriptor = new ServiceDescriptor(serviceType, _instance);
            services.Add(serviceDescriptor);
        }
    }

    #endregion
}
