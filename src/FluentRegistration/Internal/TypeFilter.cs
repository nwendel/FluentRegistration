using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using FluentRegistration.Infrastructure;

namespace FluentRegistration.Internal
{
    public class TypeFilter : ITypeFilter
    {
        #region Constructor

        public TypeFilter(Type type)
        {
            ImplementationType = type;
        }

        #endregion

        #region Assignable To

        public bool AssignableTo(Type type)
        {
            GuardAgainst.Null(type, nameof(type));

            var typeInfo = type.GetTypeInfo();
            if (typeInfo.IsGenericTypeDefinition)
            {
                if (typeInfo.IsInterface)
                {
                    return AssignableToGenericInterface(typeInfo);
                }

                return AssignableToGenericClass(typeInfo);
            }

            return type.GetTypeInfo().IsAssignableFrom(ImplementationType);
        }

        public bool AssignableTo<T>()
        {
            return AssignableTo(typeof(T));
        }

        private bool AssignableToGenericInterface(Type type)
        {
            var interfaces = ImplementationType.GetInterfaces();
            foreach (var @interface in interfaces)
            {
                var interfaceTypeInfo = @interface.GetTypeInfo();
                if (interfaceTypeInfo.IsGenericType && @interface.GetGenericTypeDefinition() == type)
                {
                    return true;
                }
            }

            return false;
        }

        private bool AssignableToGenericClass(Type type)
        {
            var candidateType = ImplementationType;
            while (candidateType != null)
            {
                var candidateTypeInfo = candidateType.GetTypeInfo();
                if (candidateTypeInfo.IsGenericType && candidateTypeInfo.GetGenericTypeDefinition() == type)
                {
                    return true;
                }

                candidateType = candidateTypeInfo.BaseType;
            }

            return false;
        }

        #endregion

        #region In Namespace

        public bool InNamespace(string @namespace)
        {
            return InNamespace(@namespace, false);
        }

        public bool InNamespace(string @namespace, bool includeSubNamespaces)
        {
            GuardAgainst.NullOrWhiteSpace(@namespace, nameof(@namespace));

            if (ImplementationType.Namespace == @namespace)
            {
                return true;
            }

            if (includeSubNamespaces)
            {
                return ImplementationType.Namespace != null &&
                       ImplementationType.Namespace.StartsWith(@namespace + ".");
            }

            return false;
        }

        #endregion

        #region In Same Namespace As

        public bool InSameNamespaceAs(Type type)
        {
            return InSameNamespaceAs(type, false);
        }

        public bool InSameNamespaceAs(Type type, bool includeSubNamespaces)
        {
            GuardAgainst.Null(type, nameof(type));

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

        #endregion

        #region In This Namespace

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
            var method = stackFrame!.GetMethod();
            var declaringType = method!.DeclaringType;

            if (declaringType == null)
            {
                throw new RegistrationException($"Unable to determine declaring type for method {method.Name}");
            }

            return InNamespace(declaringType.Namespace!, includeSubNamespaces);
        }

        #endregion

        #region Implementation Type

        public Type ImplementationType { get; }

        #endregion
    }
}
