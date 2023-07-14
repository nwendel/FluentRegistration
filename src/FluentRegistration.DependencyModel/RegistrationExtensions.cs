using System.Reflection;
using FluentRegistration.Internal;

namespace FluentRegistration;

public static class RegistrationExtensions
{
    public static ITypeSelector FromDependencyContext(this IRegistration self, DependencyContext dependencyContext)
    {
        GuardAgainst.Null(self);
        GuardAgainst.Null(dependencyContext);

        var assemblies = dependencyContext.RuntimeLibraries
            .SelectMany(x => x.GetDefaultAssemblyNames(dependencyContext))
            .Select(x => Assembly.Load(x))
            .ToList();

        return self.FromAssemblies(assemblies);
    }

    public static ITypeSelector FromDefaultDependencyContext(this IRegistration self)
    {
        if (DependencyContext.Default == null)
        {
            throw new InvalidOperationException("No default dependency context");
        }

        return self.FromDependencyContext(DependencyContext.Default);
    }
}
