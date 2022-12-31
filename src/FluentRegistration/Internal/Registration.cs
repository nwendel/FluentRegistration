using System.Reflection;
using System.Runtime.CompilerServices;
using FluentRegistration.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace FluentRegistration.Internal;

public class Registration : IRegistration
{
    private IRegister? _register;

    public IComponentImplementationSelector<TService> For<TService>()
        where TService : class
    {
        var registration = new ComponentRegistration<TService>(new[] { typeof(TService) });
        _register = registration;
        return registration;
    }

    public IComponentImplementationSelector<object> For(Type type)
    {
        GuardAgainst.Null(type);

        return For(new[] { type });
    }

    public IComponentImplementationSelector<object> For(params Type[] types)
    {
        GuardAgainst.NullOrEmpty(types);

        var registration = new ComponentRegistration<object>(types);
        _register = registration;
        return registration;
    }

    public ITypeSelector FromAssembly(Assembly assembly)
    {
        GuardAgainst.Null(assembly);

        var registration = new AssemblyTypeSelector(assembly);
        _register = registration;
        return registration;
    }

    public ITypeSelector FromAssemblyContaining(Type type)
    {
        GuardAgainst.Null(type);

        var assembly = type.Assembly;
        return FromAssembly(assembly);
    }

    public ITypeSelector FromAssemblyContaining<T>()
    {
        return FromAssemblyContaining(typeof(T));
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public ITypeSelector FromThisAssembly()
    {
        return FromAssembly(Assembly.GetCallingAssembly());
    }

    public IWithServicesInitial ImplementedBy<T>()
    {
        return FromAssemblyContaining<T>()
            .Where(c => c.ImplementationType == typeof(T));
    }

    public IWithServicesInitial Instance<T>(T instance)
        where T : class
    {
        var registration = new InstanceRegistration<T>(instance);
        _register = registration;
        return registration;
    }

    public void Register(IServiceCollection services)
    {
        if (_register == null)
        {
            throw new InvalidOperationException("Register called without defining what to register via the fluent Api.");
        }

        _register.Register(services);
    }
}
