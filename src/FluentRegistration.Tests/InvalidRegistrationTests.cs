using FluentRegistration.Tests.Classes;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace FluentRegistration.Tests;

public class InvalidRegistrationTests
{
    [Fact]
    public void ThrowsOnNullRegistrations()
    {
        var tested = new ServiceCollection();

        Assert.Throws<ArgumentNullException>("registrationAction", () =>
            tested.Register(null));
    }

    [Fact]
    public void ThrowsOnNullServiceCollection()
    {
        ServiceCollection tested = null;

        Assert.Throws<ArgumentNullException>("self", () =>
            tested.Register(r => r
                .For<ISimpleService>()
                .ImplementedBy<SimpleService>()));
    }

    [Fact]
    public void ThrowsOnNullType()
    {
        var tested = new ServiceCollection();

        Assert.Throws<ArgumentNullException>("type", () =>
            tested.Register(c => c
                .For((Type)null)
                .ImplementedBy<SimpleService>()));
    }

    [Fact]
    public void ThrowsOnNoTypes()
    {
        var tested = new ServiceCollection();

        Assert.Throws<ArgumentNullException>(() =>
            tested.Register(c => c
                .For()
                .ImplementedBy<SimpleService>()));
    }

    [Fact]
    public void ThrowsOnNoAssembly()
    {
        var tested = new ServiceCollection();

        Assert.Throws<ArgumentNullException>("assembly", () =>
            tested.Register(r => r
                .FromAssembly(null)
                .WithServices.AllInterfaces()));
    }
}
