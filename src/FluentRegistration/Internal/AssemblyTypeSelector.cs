using System;
using System.Collections.Generic;
using System.Reflection;

namespace FluentRegistration.Internal;

public class AssemblyTypeSelector : AbstractTypeSelector
{
    #region Fields

    private readonly Assembly _assembly;

    #endregion

    #region Constructor

    public AssemblyTypeSelector(Assembly assembly)
    {
        _assembly = assembly;
    }

    #endregion

    #region Types

    protected override IEnumerable<Type> Types => _assembly.GetTypes();

    #endregion
}
