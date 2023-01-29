using System.Reflection;

namespace FluentRegistration.Internal;

public class AssemblyTypeSelector : AbstractTypeSelector
{
    private readonly IEnumerable<Assembly> _assemblies;

    public AssemblyTypeSelector(IEnumerable<Assembly> assemblies)
    {
        _assemblies = assemblies;
    }

    protected override IEnumerable<Type> Types => _assemblies.SelectMany(x => x.GetTypes());
}
