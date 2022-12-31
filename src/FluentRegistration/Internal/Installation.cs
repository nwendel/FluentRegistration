using System.Reflection;
using System.Runtime.CompilerServices;
using FluentRegistration.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace FluentRegistration.Internal;

public class Installation : IInstallation
{
    private IServiceInstaller[]? _installers;

    public void FromAssembly(Assembly assembly)
    {
        GuardAgainst.Null(assembly);

        var allTypes = assembly.GetTypes();
        var installers = allTypes
            .Where(x => typeof(IServiceInstaller).IsAssignableFrom(x))
            .Select(x => Activator.CreateInstance(x))
            .Cast<IServiceInstaller>()
            .ToArray();
        _installers = installers;
    }

    public void FromAssemblyContaining(Type type)
    {
        GuardAgainst.Null(type);

        var assembly = type.Assembly;
        FromAssembly(assembly);
    }

    public void FromAssemblyContaining<T>()
    {
        FromAssemblyContaining(typeof(T));
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public void FromThisAssembly()
    {
        FromAssembly(Assembly.GetCallingAssembly());
    }

    public void Install(IServiceCollection services)
    {
        if (_installers == null)
        {
            throw new InvalidOperationException("Install called without defining what to install via the fluent Api.");
        }

        foreach (var installer in _installers)
        {
            installer.Install(services);
        }
    }
}
