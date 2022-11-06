using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentRegistration.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace FluentRegistration.Internal;

public abstract class AbstractTypeSelector :
    ITypeSelector,
    IRegister
{
    #region Fields

    private readonly List<Func<ITypeFilter, bool>> _wherePredicates = new List<Func<ITypeFilter, bool>>();
    private readonly List<Func<ITypeFilter, bool>> _exceptPredicates = new List<Func<ITypeFilter, bool>>();
    private ServiceTypeSelector _serviceTypeSelector = new ServiceTypeSelector();

    #endregion

    #region Types

    protected abstract IEnumerable<Type> Types { get; }

    #endregion

    #region Where

    public ITypeSelector Where(Func<ITypeFilter, bool> predicate)
    {
        GuardAgainst.Null(predicate);

        _wherePredicates.Add(predicate);
        return this;
    }

    #endregion

    #region Except

    public ITypeSelector Except(Func<ITypeFilter, bool> predicate)
    {
        GuardAgainst.Null(predicate);

        _exceptPredicates.Add(predicate);
        return this;
    }

    #endregion

    #region Filtered Types

    public IEnumerable<Type> FilteredTypes
    {
        get
        {
            return Types
                .Where(type =>
                {
                    var typeInfo = type.GetTypeInfo();
                    return typeInfo.IsClass && !typeInfo.IsAbstract;
                })
                .Where(type => _wherePredicates.Count == 0 || _wherePredicates.Any(filter => filter(new TypeFilter(type))))
                .Where(type => _exceptPredicates.Count == 0 || _exceptPredicates.None(filter => filter(new TypeFilter(type))));
        }
    }

    #endregion

    #region With Services

    public IServiceSelector WithServices
    {
        get
        {
            return _serviceTypeSelector;
        }
    }

    #endregion

    #region Register

    public void Register(IServiceCollection services)
    {
        var filteredTypes = FilteredTypes;

        foreach (var type in filteredTypes)
        {
            var serviceTypes = _serviceTypeSelector.GetServicesFor(type);
            var serviceLifetimeSelector = _serviceTypeSelector.GetLifetimeSelector();

            var componentRegistration = new ComponentImplementedByRegistration<object, object>(serviceTypes, type, serviceLifetimeSelector);
            componentRegistration.Register(services);
        }
    }

    #endregion
}
