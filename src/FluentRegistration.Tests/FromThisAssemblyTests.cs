using FluentRegistration.Tests.Classes.AnotherNamespace;

namespace FluentRegistration.Tests;

public class FromThisAssemblyTests
{
    [Fact]
    public void CanRegister()
    {
        var tested = new ServiceCollection();

        tested.Register(r => r
            .FromThisAssembly()
            .Where(c => c.InSameNamespaceAs<ServiceInAnotherNamespace>())
            .WithServices.AllInterfaces()
            .Lifetime.Singleton());

        Assert.Single(tested);
        Assert.All(tested, service =>
        {
            Assert.Equal(typeof(IServiceInAnotherNamespace), service.ServiceType);
            Assert.Equal(typeof(ServiceInAnotherNamespace), service.ImplementationType);
        });
    }
}
