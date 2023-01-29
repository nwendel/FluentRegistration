using System.Reflection;
using FluentRegistration.Infrastructure;
using FluentRegistration.Internal;
using Microsoft.Extensions.DependencyModel;

namespace FluentRegistration;

public static class IRegistrationExtensions
{
    public static ITypeSelector FromDependencyContext(this IRegistration self, DependencyContext dependencyContext)
    {
        GuardAgainst.Null(dependencyContext);

        var assemblies = dependencyContext.RuntimeLibraries
            .SelectMany(x => x.GetDefaultAssemblyNames(dependencyContext))
            .Select(x => Assembly.Load(x))
            .ToList();

        return self.FromAssemblies(assemblies);
    }

    public static ITypeSelector FromDefaultDependencyContext(this IRegistration self) =>
        self.FromDependencyContext(DependencyContext.Default);
}
