using System;
using FluentRegistration.Infrastructure;

namespace FluentRegistration.Internal
{
    public interface ITypeFilter :
        IFluentInterface
    {
        #region Assignable To

        bool AssignableTo(Type type);

        bool AssignableTo<T>();

        #endregion

        #region In Namespace

        bool InNamespace(string @namespace);

        #endregion

        #region In Same Namespace As

        bool InSameNamespaceAs(Type type);

        bool InSameNamespaceAs<T>();

        #endregion

        #region In This Namespace

        bool InThisNamespace();

        #endregion

        #region Implementation Type

        Type ImplementationType { get; }

        #endregion
    }
}
