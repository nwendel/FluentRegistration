using System;
using System.Reflection;

namespace FluentRegistration.Internal
{
    public interface IInstallation :
        IFluentInterface
    {
        #region From Assembly

        void FromAssembly(Assembly assembly);

        #endregion

        #region From Assembly Containing

        void FromAssemblyContaining(Type type);

        void FromAssemblyContaining<T>();

        #endregion

        #region From This Assembly

        void FromThisAssembly();

        #endregion
    }
}
