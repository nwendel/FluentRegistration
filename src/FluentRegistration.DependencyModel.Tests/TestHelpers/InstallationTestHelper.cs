using System.Reflection;
using FluentRegistration.Internal;

namespace FluentRegistration.DependencyModel.Tests.TestHelpers;

public class InstallationTestHelper : IInstallation
{
    public IEnumerable<Assembly> Assemblies { get; private set; } = Enumerable.Empty<Assembly>();

    public void FromAssemblies(IEnumerable<Assembly> assemblies)
    {
        Assemblies = assemblies;
    }

    public void FromAssemblies(params Assembly[] assemblies)
    {
        throw new NotImplementedException();
    }

    public void FromAssembly(Assembly assembly)
    {
        throw new NotImplementedException();
    }

    public void FromAssemblyContaining(Type type)
    {
        throw new NotImplementedException();
    }

    public void FromAssemblyContaining<T>()
    {
        throw new NotImplementedException();
    }

    public void FromThisAssembly()
    {
        throw new NotImplementedException();
    }
}
