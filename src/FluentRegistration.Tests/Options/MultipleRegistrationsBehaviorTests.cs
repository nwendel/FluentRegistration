using FluentRegistration.Options;
using FluentRegistration.Tests.Classes;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace FluentRegistration.Tests.Options;

public class MultipleRegistrationsBehaviorTests
{
    [Fact]
    public void CanRegisterSameImplementedByTwice()
    {
        var tested = new ServiceCollection();

        tested.Register(c => c
            .For<ISimpleService>()
            .ImplementedBy<SimpleService>());
        tested.Register(c => c
            .For<ISimpleService>()
            .ImplementedBy<SimpleService>());

        Assert.Single(tested);
        Assert.All(tested, service =>
        {
            Assert.Equal(ServiceLifetime.Singleton, service.Lifetime);
            Assert.Equal(typeof(ISimpleService), service.ServiceType);
            Assert.Equal(typeof(SimpleService), service.ImplementationType);
        });
    }

    [Fact]
    public void CanRegisterSameImplementedByTwiceIgnore()
    {
        var tested = new ServiceCollection();
        tested.Configure(c => c.MultipleRegistrationsBehavior = MultipleRegistrationsBehavior.Ignore);

        tested.Register(c => c
            .For<ISimpleService>()
            .ImplementedBy<SimpleService>());
        tested.Register(c => c
            .For<ISimpleService>()
            .ImplementedBy<SimpleService>());

        Assert.Single(tested);
        Assert.All(tested, service =>
        {
            Assert.Equal(ServiceLifetime.Singleton, service.Lifetime);
            Assert.Equal(typeof(ISimpleService), service.ServiceType);
            Assert.Equal(typeof(SimpleService), service.ImplementationType);
        });
    }

    [Fact]
    public void CanRegisterSameImplementedByTwiceRegister()
    {
        var tested = new ServiceCollection();
        tested.Configure(c => c.MultipleRegistrationsBehavior = MultipleRegistrationsBehavior.Register);

        tested.Register(c => c
            .For<ISimpleService>()
            .ImplementedBy<SimpleService>());
        tested.Register(c => c
            .For<ISimpleService>()
            .ImplementedBy<SimpleService>());

        Assert.Equal(2, tested.Count);
        Assert.All(tested, service =>
        {
            Assert.Equal(ServiceLifetime.Singleton, service.Lifetime);
            Assert.Equal(typeof(ISimpleService), service.ServiceType);
            Assert.Equal(typeof(SimpleService), service.ImplementationType);
        });
    }

    [Fact]
    public void CanRegisterSameImplementedByTwiceThrow()
    {
        var tested = new ServiceCollection();
        tested.Configure(c => c.MultipleRegistrationsBehavior = MultipleRegistrationsBehavior.ThrowException);

        tested.Register(c => c
            .For<ISimpleService>()
            .ImplementedBy<SimpleService>());

        Assert.Throws<RegistrationException>(() =>
            tested.Register(c => c
                .For<ISimpleService>()
                .ImplementedBy<SimpleService>()));
    }
}
