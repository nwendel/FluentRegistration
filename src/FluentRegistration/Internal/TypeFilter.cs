using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace FluentRegistration.Internal;

public class TypeFilter : ITypeFilter
{
    public TypeFilter(Type type)
    {
        ImplementationType = type;
    }

    public Type ImplementationType { get; }

    public bool AssignableTo(Type type)
    {
        GuardAgainst.Null(type);

        if (type.IsGenericTypeDefinition)
        {
            if (type.IsInterface)
            {
                return AssignableToGenericInterface(type);
            }

            return AssignableToGenericClass(type);
        }

        return type.IsAssignableFrom(ImplementationType);
    }

    public bool AssignableTo<T>()
    {
        return AssignableTo(typeof(T));
    }

    public bool AssignableToGenericInterface(Type type)
    {
        var interfaces = ImplementationType.GetInterfaces();
        foreach (var @interface in interfaces)
        {
            if (@interface.IsGenericType && @interface.GetGenericTypeDefinition() == type)
            {
                return true;
            }
        }

        return false;
    }

    public bool AssignableToGenericClass(Type type)
    {
        var candidateType = ImplementationType;
        while (candidateType != null)
        {
            if (candidateType.IsGenericType && candidateType.GetGenericTypeDefinition() == type)
            {
                return true;
            }

            candidateType = candidateType.BaseType;
        }

        return false;
    }

    public bool InNamespace(string @namespace)
    {
        return InNamespace(@namespace, false);
    }

    public bool InNamespace(string @namespace, bool includeSubNamespaces)
    {
        GuardAgainst.NullOrWhiteSpace(@namespace);

        if (ImplementationType.Namespace == @namespace)
        {
            return true;
        }

        if (includeSubNamespaces)
        {
            return ImplementationType.Namespace != null &&
                   ImplementationType.Namespace.StartsWith(@namespace + ".", StringComparison.Ordinal);
        }

        return false;
    }

    public bool InSameNamespaceAs(Type type)
    {
        return InSameNamespaceAs(type, false);
    }

    public bool InSameNamespaceAs(Type type, bool includeSubNamespaces)
    {
        GuardAgainst.Null(type);

        return InNamespace(type.Namespace!, includeSubNamespaces);
    }

    public bool InSameNamespaceAs<T>()
    {
        return InSameNamespaceAs<T>(false);
    }

    public bool InSameNamespaceAs<T>(bool includeSubNamespaces)
    {
        return InSameNamespaceAs(typeof(T), includeSubNamespaces);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public bool InThisNamespace()
    {
        return InThisNamespaceCore(false);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public bool InThisNamespace(bool includeSubNamespaces)
    {
        return InThisNamespaceCore(includeSubNamespaces);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public bool InThisNamespaceCore(bool includeSubNamespaces)
    {
        var stackTrace = new StackTrace();
        var stackFrame = stackTrace.GetFrame(2);
        if (stackFrame == null)
        {
            throw new RegistrationException($"Unable to get callstack");
        }

        var method = stackFrame.GetMethod();
        if (method == null)
        {
            throw new RegistrationException($"Unable to get method");
        }

        var declaringType = method.DeclaringType;
        if (declaringType == null)
        {
            throw new RegistrationException($"Unable to determine declaring type for method {method.Name}");
        }

        var @namespace = declaringType.Namespace;
        if (@namespace == null)
        {
            return false;
        }

        return InNamespace(@namespace, includeSubNamespaces);
    }
}
