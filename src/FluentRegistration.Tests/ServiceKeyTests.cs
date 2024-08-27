using FluentRegistration.Tests.Classes;

namespace FluentRegistration.Tests;

public class ServiceKeyTests
{
    [Fact]
    public void CanRegister()
    {
        var tested = new ServiceCollection();

        tested.Register(r => r
            .ImplementedBy<SimpleService>()
            .WithServices.AllInterfaces()
            .Lifetime.Singleton()
            .HasServiceKey.ImplementationType());

        Assert.Single(tested);
        Assert.All(tested, service =>
        {
            Assert.Equal(typeof(ISimpleService), service.ServiceType);
            Assert.Equal(typeof(SimpleService), service.KeyedImplementationType);
            Assert.Equal(typeof(SimpleService), service.ServiceKey);
        });
    }

    [Fact]
    public void CanResolveSameInstance()
    {
        var tested = new ServiceCollection();

        tested.Register(r => r
            .ImplementedBy<TwoInterfacesService>()
            .WithServices.AllInterfaces()
            .Lifetime.Singleton()
            .HasServiceKey.ImplementationType());

        var provider = tested.BuildServiceProvider();

        var instance1 = provider.GetRequiredKeyedService<IInterfaceOne>(typeof(TwoInterfacesService));
        var instance2 = provider.GetRequiredKeyedService<IInterfaceTwo>(typeof(TwoInterfacesService));

        Assert.Same(instance1, instance2);
    }
}
