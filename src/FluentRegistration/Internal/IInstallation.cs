using System.Reflection;
using FluentRegistration.Infrastructure;

namespace FluentRegistration.Internal;

public interface IInstallation :
    IFluentInterface
{
    void FromAssembly(Assembly assembly);

    void FromAssemblyContaining(Type type);

    void FromAssemblyContaining<T>();

    void FromThisAssembly();
}
