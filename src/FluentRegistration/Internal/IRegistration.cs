using System.Reflection;
using FluentRegistration.Infrastructure;

namespace FluentRegistration.Internal;

public interface IRegistration :
    IFluentInterface
{
    IComponentImplementationSelector<TService> For<TService>()
        where TService : class;

    IComponentImplementationSelector<object> For(Type type);

    IComponentImplementationSelector<object> For(params Type[] types);

    ITypeSelector FromAssembly(Assembly assembly);

    ITypeSelector FromAssemblyContaining(Type type);

    ITypeSelector FromAssemblyContaining<T>();

    ITypeSelector FromThisAssembly();

    IWithServicesInitial ImplementedBy<T>();

    IWithServicesInitial Instance<T>(T instance)
        where T : class;
}
