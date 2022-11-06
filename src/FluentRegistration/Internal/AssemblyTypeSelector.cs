using System;
using System.Collections.Generic;
using System.Reflection;

namespace FluentRegistration.Internal;

public class AssemblyTypeSelector : AbstractTypeSelector
{
    private readonly Assembly _assembly;

    public AssemblyTypeSelector(Assembly assembly)
    {
        _assembly = assembly;
    }

    protected override IEnumerable<Type> Types => _assembly.GetTypes();
}
