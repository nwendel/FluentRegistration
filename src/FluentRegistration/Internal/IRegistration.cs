using System;
using System.Reflection;
using FluentRegistration.Infrastructure;

namespace FluentRegistration.Internal;

public interface IRegistration :
    IFluentInterface
{
    #region For

    IComponentImplementationSelector<TService> For<TService>()
        where TService : class;

    IComponentImplementationSelector<object> For(Type type);

    IComponentImplementationSelector<object> For(params Type[] types);

    #endregion

    #region From Assembly

    ITypeSelector FromAssembly(Assembly assembly);

    #endregion

    #region From Assembly Containing

    ITypeSelector FromAssemblyContaining(Type type);

    ITypeSelector FromAssemblyContaining<T>();

    #endregion

    #region From This Assembly

    ITypeSelector FromThisAssembly();

    #endregion

    #region Implemented By

    IWithServicesInitial ImplementedBy<T>();

    #endregion

    #region Instance

    IWithServicesInitial Instance<T>(T instance)
        where T : class;

    #endregion
}
