using System;
using FluentRegistration.Tests.Classes;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace FluentRegistration.Tests;

public class InstanceTests
{
    [Fact]
    public void ThrowsOnRegisterNullInstance()
    {
        var tested = new ServiceCollection();

        Assert.Throws<ArgumentNullException>("instance", () =>
            tested.Register(r => r
                .Instance<SimpleService>(null)
                .WithServices.AllInterfaces()));
    }

    [Fact]
    public void CanRegister()
    {
        var tested = new ServiceCollection();
        var instance = new SimpleService();

        tested.Register(r => r
            .Instance(instance)
            .WithServices.AllInterfaces());

        Assert.Single(tested);
        Assert.All(tested, service =>
        {
            Assert.Equal(typeof(ISimpleService), service.ServiceType);
            Assert.Same(instance, service.ImplementationInstance);
        });
    }

    [Fact]
    public void CanRegisterDeclared()
    {
        var tested = new ServiceCollection();
        var instance = new SimpleService();

        tested.Register(r => r
            .Instance<object>(instance)
            .WithServices.AllInterfaces());

        Assert.Single(tested);
        Assert.All(tested, service =>
        {
            Assert.Equal(typeof(ISimpleService), service.ServiceType);
            Assert.Same(instance, service.ImplementationInstance);
        });
    }
}
