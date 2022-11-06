using FluentRegistration.Tests.Classes.AnotherNamespace;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

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
            .WithServices.AllInterfaces());

        Assert.Single(tested);
        Assert.All(tested, service =>
        {
            Assert.Equal(typeof(IServiceInAnotherNamespace), service.ServiceType);
            Assert.Equal(typeof(ServiceInAnotherNamespace), service.ImplementationType);
        });
    }
}
