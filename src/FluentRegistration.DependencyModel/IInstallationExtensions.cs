using System.Reflection;
using FluentRegistration.Internal;

namespace FluentRegistration;

public static class IInstallationExtensions
{
    public static void FromDependencyContext(this IInstallation self, DependencyContext dependencyContext)
    {
        GuardAgainst.Null(self);
        GuardAgainst.Null(dependencyContext);

        var assemblies = dependencyContext.RuntimeLibraries
            .SelectMany(x => x.GetDefaultAssemblyNames(dependencyContext))
            .Select(x => Assembly.Load(x))
            .ToList();

        self.FromAssemblies(assemblies);
    }

    public static void FromDefaultDependencyContext(this IInstallation self)
    {
        if (DependencyContext.Default == null)
        {
            throw new InvalidOperationException("No default depency context");
        }

        self.FromDependencyContext(DependencyContext.Default);
    }
}
