using FluentRegistration.Tests.Classes;

namespace FluentRegistration.Tests;

public class WithServicesTests
{
    [Fact]
    public void CanRegisterWithServicesSelf()
    {
        var tested = new ServiceCollection();

        tested.Register(r => r
            .FromAssemblyContaining<SimpleService>()
            .Where(c => c.ImplementationType == typeof(SimpleService))
            .WithServices.Self()
            .Lifetime.Singleton());

        Assert.Single(tested);
        Assert.All(tested, service =>
        {
            Assert.Equal(typeof(SimpleService), service.ServiceType);
            Assert.Equal(typeof(SimpleService), service.ImplementationType);
        });
    }

    [Fact]
    public void CanRegisterWithDefaultInterface()
    {
        var tested = new ServiceCollection();

        tested.Register(r => r
            .FromAssemblyContaining<SimpleService>()
            .Where(c => c.ImplementationType == typeof(SimpleService))
            .WithServices.DefaultInterface()
            .Lifetime.Singleton());

        Assert.Single(tested);
        Assert.All(tested, service =>
        {
            Assert.Equal(typeof(ISimpleService), service.ServiceType);
            Assert.Equal(typeof(SimpleService), service.ImplementationType);
        });
    }

    [Fact]
    public void CanRegisterWithSpecificInterface()
    {
        var tested = new ServiceCollection();

        tested.Register(r => r
            .FromAssemblyContaining<SimpleService>()
            .Where(c => c.ImplementationType == typeof(SimpleService))
            .WithServices.Service<ISimpleService>()
            .Lifetime.Singleton());

        Assert.Single(tested);
        Assert.All(tested, service =>
        {
            Assert.Equal(typeof(ISimpleService), service.ServiceType);
            Assert.Equal(typeof(SimpleService), service.ImplementationType);
        });
    }

    [Fact]
    public void CanRegisterWithServicesDefaultInterfaceSelf()
    {
        var tested = new ServiceCollection();

        tested.Register(r => r
            .FromAssemblyContaining<SimpleService>()
            .Where(c => c.ImplementationType == typeof(SimpleService))
            .WithServices
                .Self()
                .DefaultInterface()
            .Lifetime.Singleton());

        Assert.Equal(2, tested.Count);
        Assert.Contains(tested, x => x.ServiceType == typeof(ISimpleService));
        Assert.Contains(tested, x => x.ServiceType == typeof(SimpleService));
    }
}
