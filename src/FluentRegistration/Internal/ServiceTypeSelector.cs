namespace FluentRegistration.Internal;

public class ServiceTypeSelector : IWithServices
{
    private readonly List<Func<Type, IEnumerable<Type>>> _serviceTypeSelectors = new();
    private readonly LifetimeSelector _lifetimeSelector = new();

    public ILifetimeSelector Lifetime => _lifetimeSelector;

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

    public LifetimeSelector GetLifetimeSelector() => _lifetimeSelector;
}
