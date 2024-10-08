﻿namespace FluentRegistration.Internal;

public abstract class AbstractTypeSelector : ITypeSelector, IRegister
{
    private readonly List<Func<ITypeFilter, bool>> _wherePredicates = [];
    private readonly List<Func<ITypeFilter, bool>> _exceptPredicates = [];
    private readonly ServiceTypeSelector _serviceTypeSelector = new();

    public IEnumerable<Type> FilteredTypes
    {
        get
        {
            return Types
                .Where(type => type.IsClass && !type.IsAbstract)
                .Where(type => _wherePredicates.Count == 0 || _wherePredicates.Any(filter => filter(new TypeFilter(type))))
                .Where(type => _exceptPredicates.Count == 0 || _exceptPredicates.None(filter => filter(new TypeFilter(type))));
        }
    }

    public IServiceSelector WithServices
    {
        get
        {
            return _serviceTypeSelector;
        }
    }

    protected abstract IEnumerable<Type> Types { get; }

    public ITypeSelector Where(Func<ITypeFilter, bool> predicate)
    {
        GuardAgainst.Null(predicate);

        _wherePredicates.Add(predicate);
        return this;
    }

    public ITypeSelector Except(Func<ITypeFilter, bool> predicate)
    {
        GuardAgainst.Null(predicate);

        _exceptPredicates.Add(predicate);
        return this;
    }

    public void Register(IServiceCollection services)
    {
        var filteredTypes = FilteredTypes;

        foreach (var type in filteredTypes)
        {
            var serviceTypes = _serviceTypeSelector.GetServicesFor(type);
            var serviceLifetimeSelector = _serviceTypeSelector.LifetimeAndServiceKeySelector;

            var componentRegistration = new ComponentImplementedByRegistration<object, object>(serviceTypes, type, serviceLifetimeSelector);
            componentRegistration.Register(services);
        }
    }
}
