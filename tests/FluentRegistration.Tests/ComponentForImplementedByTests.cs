using FluentRegistration.Tests.Classes;

namespace FluentRegistration.Tests;

public class ComponentForImplementedByTests
{
    [Fact]
    public void CanRegisterSingletonLifetime()
    {
        var tested = new ServiceCollection();

        tested.Register(r => r
            .For<ISimpleService>()
            .ImplementedBy<SimpleService>()
            .Lifetime.Singleton());

        Assert.Single(tested);
        Assert.All(tested, service =>
        {
            Assert.Equal(ServiceLifetime.Singleton, service.Lifetime);
            Assert.Equal(typeof(ISimpleService), service.ServiceType);
            Assert.Equal(typeof(SimpleService), service.ImplementationType);
        });
    }

    [Fact]
    public void CanRegisterScopedLifetime()
    {
        var tested = new ServiceCollection();

        tested.Register(r => r
            .For<ISimpleService>()
            .ImplementedBy<SimpleService>()
            .Lifetime.Scoped());

        Assert.Single(tested);
        Assert.All(tested, service =>
        {
            Assert.Equal(ServiceLifetime.Scoped, service.Lifetime);
            Assert.Equal(typeof(ISimpleService), service.ServiceType);
            Assert.Equal(typeof(SimpleService), service.ImplementationType);
        });
    }

    [Fact]
    public void CanRegisterTransientLifetime()
    {
        var tested = new ServiceCollection();

        tested.Register(r => r
            .For<ISimpleService>()
            .ImplementedBy<SimpleService>()
            .Lifetime.Transient());

        Assert.Single(tested);
        Assert.All(tested, service =>
        {
            Assert.Equal(ServiceLifetime.Transient, service.Lifetime);
            Assert.Equal(typeof(ISimpleService), service.ServiceType);
            Assert.Equal(typeof(SimpleService), service.ImplementationType);
        });
    }

    [Fact]
    public void CanRegisterForNonGeneric()
    {
        var tested = new ServiceCollection();

        tested.Register(c => c
                .For(typeof(ISimpleService))
                .ImplementedBy<SimpleService>()
                .Lifetime.Singleton());

        Assert.Single(tested);
        Assert.All(tested, service =>
        {
            Assert.Equal(ServiceLifetime.Singleton, service.Lifetime);
            Assert.Equal(typeof(ISimpleService), service.ServiceType);
            Assert.Equal(typeof(SimpleService), service.ImplementationType);
        });
    }
}
