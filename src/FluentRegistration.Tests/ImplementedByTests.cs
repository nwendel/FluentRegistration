using FluentRegistration.Tests.Classes;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace FluentRegistration.Tests;

public class ImplementedByTests
{
    [Fact]
    public void CanRegister()
    {
        var tested = new ServiceCollection();

        tested.Register(r => r
            .ImplementedBy<SimpleService>()
            .WithServices.AllInterfaces());

        Assert.Single(tested);
        Assert.All(tested, service =>
        {
            Assert.Equal(typeof(ISimpleService), service.ServiceType);
            Assert.Equal(typeof(SimpleService), service.ImplementationType);
        });
    }
}
