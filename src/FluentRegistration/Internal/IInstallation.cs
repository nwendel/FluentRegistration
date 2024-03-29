﻿using System.Reflection;

namespace FluentRegistration.Internal;

public interface IInstallation : IFluentInterface
{
    void FromAssemblies(IEnumerable<Assembly> assemblies);

    void FromAssemblies(params Assembly[] assemblies);

    void FromAssembly(Assembly assembly);

    void FromAssemblyContaining(Type type);

    void FromAssemblyContaining<T>();

    void FromThisAssembly();
}
