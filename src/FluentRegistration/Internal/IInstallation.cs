using System.Reflection;

namespace FluentRegistration.Internal;

public interface IInstallation : IFluentInterface
{
    void FromAssembly(Assembly assembly);

    void FromAssemblyContaining(Type type);

    void FromAssemblyContaining<T>();

    void FromThisAssembly();
}
