using FluentRegistration.Tests.Classes;

namespace FluentRegistration.Tests;

public class FactoryTests
{
    [Fact]
    public void ThrowsOnRegisterUsingFactoryNullFactoryMethod()
    {
        var tested = new ServiceCollection();

        Assert.Throws<ArgumentNullException>("factoryMethod", () =>
            tested.Register(c => c
            .For<ISimpleService>()
            .UsingFactory<SimpleServiceServiceFactory>(null!)
            .Lifetime.Singleton()));
    }

    [Fact]
    public void CanRegisterUsingFactory()
    {
        var tested = new ServiceCollection();

        tested.Register(c => c
            .For<SimpleServiceServiceFactory>()
            .ImplementedBy<SimpleServiceServiceFactory>()
            .Lifetime.Singleton());
        tested.Register(c => c
            .For<ISimpleService>()
            .UsingFactory<SimpleServiceServiceFactory>(f => f.CreateSimpleService())
            .Lifetime.Singleton());
        var serviceProvider = tested.BuildServiceProvider();
        var service = serviceProvider.GetService<ISimpleService>();

        Assert.Same(SimpleServiceServiceFactory.SimpleService, service);
    }

    [Fact]
    public void CanRegisterUsingFactoryMethod()
    {
        var tested = new ServiceCollection();

        var simpleService = new SimpleService();
        tested.Register(c => c
                .For<ISimpleService>()
                .UsingFactoryMethod(() => simpleService)
                .Lifetime.Singleton());
        var serviceProvider = tested.BuildServiceProvider();
        var service = serviceProvider.GetService<ISimpleService>();

        Assert.Same(simpleService, service);
    }

    [Fact]
    public void ThrowsOnUsingFactoryMethod()
    {
        var tested = new ServiceCollection();

        Assert.Throws<ArgumentNullException>("factoryMethod", () =>
            tested.Register(c => c
                .For<ISimpleService>()
                .UsingFactoryMethod((Func<ISimpleService>)null!)
                .Lifetime.Singleton()));
    }

    [Fact]
    public void ThrowsOnUsingFactoryMethodWithServiceProvider()
    {
        var tested = new ServiceCollection();

        Assert.Throws<ArgumentNullException>("factoryMethod", () =>
            tested.Register(c => c
                .For<ISimpleService>()
                .UsingFactoryMethod((Func<IServiceProvider, ISimpleService>)null!)
                .Lifetime.Singleton()));
    }
}
