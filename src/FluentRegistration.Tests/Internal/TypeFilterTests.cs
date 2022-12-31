using FluentRegistration.Internal;
using FluentRegistration.Tests.Classes;
using Xunit;

namespace FluentRegistration.Tests.Internal;

public class TypeFilterTests
{
    [Fact]
    public void ThrowsOnInNamespaceNull()
    {
        var tested = new TypeFilter(typeof(SimpleService));

        Assert.Throws<ArgumentNullException>("namespace", () => tested.InNamespace(null));
    }

    [Fact]
    public void ThrowsOnAssignableToNull()
    {
        var tested = new TypeFilter(typeof(SimpleService));

        Assert.Throws<ArgumentNullException>("type", () => tested.AssignableTo(null));
    }

    [Fact]
    public void CanAssignableTo()
    {
        var tested = new TypeFilter(typeof(SimpleService));

        Assert.True(tested.AssignableTo<ISimpleService>());
    }

    [Fact]
    public void CanAssignableToGenericClassTrue()
    {
        var tested = new TypeFilter(typeof(GenericServiceThree<>));

        Assert.True(tested.AssignableTo(typeof(GenericServiceOne<>)));
    }

    [Fact]
    public void CanAssignableToGenericClassFalse()
    {
        var tested = new TypeFilter(typeof(object));

        Assert.False(tested.AssignableTo(typeof(GenericServiceOne<>)));
    }

    [Fact]
    public void CanAssignableToGenericInterfaceTrue()
    {
        var tested = new TypeFilter(typeof(GenericServiceThree<>));

        Assert.True(tested.AssignableTo(typeof(IGenericInterface<>)));
    }

    [Fact]
    public void CanAssignableToGenericInterfaceFalse()
    {
        var tested = new TypeFilter(typeof(object));

        Assert.False(tested.AssignableTo(typeof(IGenericInterface<>)));
    }
}
