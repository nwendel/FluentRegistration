using System.Reflection;
using FluentRegistration.Internal;

namespace FluentRegistration.DependencyModel.Tests.TestHelpers;

public class RegistrationTestHelper : IRegistration
{
    public IEnumerable<Assembly> Assemblies { get; private set; } = Enumerable.Empty<Assembly>();

    public IComponentImplementationSelector<TService> For<TService>()
        where TService : class
    {
        throw new NotImplementedException();
    }

    public IComponentImplementationSelector<object> For(Type type)
    {
        throw new NotImplementedException();
    }

    public IComponentImplementationSelector<object> For(params Type[] types)
    {
        throw new NotImplementedException();
    }

    public ITypeSelector FromAssemblies(IEnumerable<Assembly> assemblies)
    {
        Assemblies = assemblies;
        return null!;
    }

    public ITypeSelector FromAssemblies(params Assembly[] assemblies)
    {
        throw new NotImplementedException();
    }

    public ITypeSelector FromAssembly(Assembly assembly)
    {
        throw new NotImplementedException();
    }

    public ITypeSelector FromAssemblyContaining(Type type)
    {
        throw new NotImplementedException();
    }

    public ITypeSelector FromAssemblyContaining<T>()
    {
        throw new NotImplementedException();
    }

    public ITypeSelector FromThisAssembly()
    {
        throw new NotImplementedException();
    }

    public IWithServicesInitial ImplementedBy<T>()
    {
        throw new NotImplementedException();
    }

    public IWithServicesInitial Instance<T>(T instance)
        where T : class
    {
        throw new NotImplementedException();
    }
}
