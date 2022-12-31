using FluentRegistration.Infrastructure;

namespace FluentRegistration.Internal;

public interface ITypeFilter :
    IFluentInterface
{
    bool AssignableTo(Type type);

    bool AssignableTo<T>();

    bool InNamespace(string @namespace);

    bool InSameNamespaceAs(Type type);

    bool InSameNamespaceAs<T>();

    bool InThisNamespace();

    Type ImplementationType { get; }
}
