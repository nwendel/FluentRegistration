﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentRegistration.Internal;

public class ServiceTypeSelector :
    IWithServices
{
    private readonly List<Func<Type, IEnumerable<Type>>> _serviceTypeSelectors = new();
    private readonly LifetimeSelector _lifetimeSelector = new();

    public IWithServices AllInterfaces()
    {
        _serviceTypeSelectors.Add(type => type.GetInterfaces());
        return this;
    }

    public IWithServices DefaultInterface()
    {
        _serviceTypeSelectors.Add(type =>
        {
            var interfaces = type.GetInterfaces();
            var defaultInterfaces = interfaces.Where(i =>
            {
                var name = i.Name;
                if (name.Length > 1 && name[0] == 'I' && char.IsUpper(name[1]))
                {
                    name = name.Substring(1);
                }

                return type.Name.Contains(name);
            });
            return defaultInterfaces;
        });

        return this;
    }

    public IWithServices Service<TService>()
    {
        _serviceTypeSelectors.Add(type => new[] { typeof(TService) });
        return this;
    }

    public IWithServices Self()
    {
        _serviceTypeSelectors.Add(type => new[] { type });
        return this;
    }

    public IEnumerable<Type> GetServicesFor(Type type)
    {
        return _serviceTypeSelectors.SelectMany(selector => selector(type));
    }

    public ILifetimeSelector Lifetime => _lifetimeSelector;

    public LifetimeSelector GetLifetimeSelector() => _lifetimeSelector;
}
