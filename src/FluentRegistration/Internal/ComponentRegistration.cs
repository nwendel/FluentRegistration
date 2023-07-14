namespace FluentRegistration.Internal;

public class ComponentRegistration<TService> : IComponentImplementationSelector<TService>, IRegister
    where TService : class
{
    private readonly Type[] _serviceTypes;
    private IRegister? _register;

    public ComponentRegistration(Type[] serviceTypes)
    {
        _serviceTypes = serviceTypes;
    }

    public ILifetime ImplementedBy<TImplementation>()
        where TImplementation : TService
    {
        var implementedByRegistration = new ComponentImplementedByRegistration<TService, TImplementation>(_serviceTypes);
        _register = implementedByRegistration;
        return implementedByRegistration;
    }

    public IValidRegistration Instance(TService instance)
    {
        GuardAgainst.Null(instance);

        var instanceRegistration = new ComponentInstanceRegistration(_serviceTypes, instance);
        _register = instanceRegistration;
        return instanceRegistration;
    }

    public ILifetime UsingFactory<TFactory>(Func<TFactory, TService> factoryMethod)
        where TFactory : class
    {
        GuardAgainst.Null(factoryMethod);

        var factoryRegistration = new ComponentFactoryRegistration<TFactory, TService>(factoryMethod);
        _register = factoryRegistration;
        return factoryRegistration;
    }

    public ILifetime UsingFactoryMethod(Func<TService> factoryMethod)
    {
        GuardAgainst.Null(factoryMethod);

        return UsingFactoryMethod(serviceProvider => factoryMethod());
    }

    public ILifetime UsingFactoryMethod(Func<IServiceProvider, TService> factoryMethod)
    {
        GuardAgainst.Null(factoryMethod);

        var factoryMethodRegistration = new ComponentFactoryMethodRegistration<TService>(factoryMethod);
        _register = factoryMethodRegistration;
        return factoryMethodRegistration;
    }

    public void Register(IServiceCollection services)
    {
        if (_register == null)
        {
            throw new InvalidOperationException("Register called without defining what to register via the fluent Api");
        }

        _register.Register(services);
    }
}
