namespace FluentRegistration.Internal;

public interface ITypeFilter : IFluentInterface
{
    Type ImplementationType { get; }

    bool AssignableTo(Type type);

    bool AssignableTo<T>();

    bool InNamespace(string @namespace);

    bool InSameNamespaceAs(Type type);

    bool InSameNamespaceAs<T>();

    bool InThisNamespace();
}
