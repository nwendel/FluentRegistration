using System.Reflection;

namespace FluentRegistration.Internal;

public interface IRegistration : IFluentInterface
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1716:Identifiers should not match keywords", Justification = "Maybe rename it in some future version?")]
    IComponentImplementationSelector<TService> For<TService>()
        where TService : class;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1716:Identifiers should not match keywords", Justification = "Maybe rename it in some future version?")]
    IComponentImplementationSelector<object> For(Type type);

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1716:Identifiers should not match keywords", Justification = "Maybe rename it in some future version?")]
    IComponentImplementationSelector<object> For(params Type[] types);

    ITypeSelector FromAssemblies(IEnumerable<Assembly> assemblies);

    ITypeSelector FromAssemblies(params Assembly[] assemblies);

    ITypeSelector FromAssembly(Assembly assembly);

    ITypeSelector FromAssemblyContaining(Type type);

    ITypeSelector FromAssemblyContaining<T>();

    ITypeSelector FromThisAssembly();

    IWithServicesInitial ImplementedBy<T>();

    IWithServicesInitial Instance<T>(T instance)
        where T : class;
}
