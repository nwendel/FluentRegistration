using FluentRegistration.Internal;

namespace FluentRegistration.Tests;

public class TypeSelectorTests
{
    [Fact]
    public void ThrowsOnNullWhere()
    {
        var tested = new AssemblyTypeSelector(null!);

        Assert.Throws<ArgumentNullException>("predicate", () => tested.Where(null!));
    }

    [Fact]
    public void ThrowsOnNullExcept()
    {
        var tested = new AssemblyTypeSelector(null!);

        Assert.Throws<ArgumentNullException>("predicate", () => tested.Except(null!));
    }
}
