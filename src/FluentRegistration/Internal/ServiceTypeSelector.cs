namespace FluentRegistration.Internal;

public class ServiceTypeSelector : IWithServices
{
    private readonly List<Func<Type, IEnumerable<Type>>> _serviceTypeSelectors = [];
    private readonly LifetimeAndServiceKeySelector<IHasServiceKeySelectorComponent> _lifetimeAndServiceKeySelector = new();

    public ILifetimeSelector<IHasServiceKeySelectorComponent> Lifetime => _lifetimeAndServiceKeySelector;

    public LifetimeAndServiceKeySelector<IHasServiceKeySelectorComponent> LifetimeAndServiceKeySelector => _lifetimeAndServiceKeySelector;

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
                    name = name[1..];
                }

                // TODO: Is "Contains" correct here?
                //       Possibly EndWith?
                return type.Name.Contains(name, StringComparison.Ordinal);
            });
            return defaultInterfaces;
        });

        return this;
    }

    public IWithServices Service<TService>()
    {
        _serviceTypeSelectors.Add(type => [typeof(TService)]);
        return this;
    }

    public IWithServices Self()
    {
        _serviceTypeSelectors.Add(type => [type]);
        return this;
    }

    public IEnumerable<Type> GetServicesFor(Type type)
    {
        return _serviceTypeSelectors.SelectMany(selector => selector(type));
    }
}
