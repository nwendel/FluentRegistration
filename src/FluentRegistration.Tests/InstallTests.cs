using FluentRegistration.Tests.Classes;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace FluentRegistration.Tests;

public class InstallTests
{
    [Fact]
    public void CanInstall()
    {
        var tested = new ServiceCollection();

        tested.Install<SimpleServiceInstaller>();

        Assert.Single(tested);
        Assert.All(tested, service =>
        {
            Assert.Equal(ServiceLifetime.Singleton, service.Lifetime);
            Assert.Equal(typeof(ISimpleService), service.ServiceType);
            Assert.Equal(typeof(SimpleService), service.ImplementationType);
        });
    }

    [Fact]
    public void CanInstallFromAssemblyContaining()
    {
        var tested = new ServiceCollection();

        tested.Install(i => i.FromAssemblyContaining<SimpleServiceInstaller>());

        Assert.Single(tested);
        Assert.All(tested, service =>
        {
            Assert.Equal(ServiceLifetime.Singleton, service.Lifetime);
            Assert.Equal(typeof(ISimpleService), service.ServiceType);
            Assert.Equal(typeof(SimpleService), service.ImplementationType);
        });
    }

    [Fact]
    public void CanInstallFromThisAssembly()
    {
        var tested = new ServiceCollection();

        tested.Install(i => i.FromThisAssembly());

        Assert.Single(tested);
        Assert.All(tested, service =>
        {
            Assert.Equal(ServiceLifetime.Singleton, service.Lifetime);
            Assert.Equal(typeof(ISimpleService), service.ServiceType);
            Assert.Equal(typeof(SimpleService), service.ImplementationType);
        });
    }

    [Fact]
    public void ThrowsOnInstallFromAssemblyNullAssembly()
    {
        var tested = new ServiceCollection();

        Assert.Throws<ArgumentNullException>("assembly", () =>
            tested.Install(i => i.FromAssembly(null)));
    }
}
