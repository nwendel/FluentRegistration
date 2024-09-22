namespace FluentRegistration.Internal;

public interface ITypeFilter : IFluentInterface
{
    Type ImplementationType { get; }

    bool AssignableTo(Type type);

    bool AssignableTo<T>();

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1716:Identifiers should not match keywords", Justification = "Parameter name is good")]
    bool InNamespace(string @namespace);

    bool InSameNamespaceAs(Type type);

    bool InSameNamespaceAs<T>();

    bool InThisNamespace();
}
