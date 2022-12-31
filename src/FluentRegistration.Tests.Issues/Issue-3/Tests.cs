using System;
using System.Linq;
using FluentRegistration.Tests.Issues.Issue_3.Classes;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace FluentRegistration.Tests.Issues.Issue_3;

public class Tests
{
    private readonly ServiceCollection _tested = new();
    private readonly IServiceProvider _serviceProvider;

    public Tests()
    {
        _tested.Register(r => r
            .FromThisAssembly()
            .Where(c => c.InSameNamespaceAs<IInterfaceOne>())
            .WithServices.AllInterfaces());
        _serviceProvider = _tested.BuildServiceProvider();
    }

    [Fact]
    public void CanResolveOne()
    {
        var services = _serviceProvider.GetServices<IInterfaceOne>();

        Assert.Equal(2, services.Count());
        Assert.Contains(services, x => x.GetType() == typeof(ServiceOne));
        Assert.Contains(services, x => x.GetType() == typeof(ServiceTwo));
    }

    [Fact]
    public void CanResolveTwo()
    {
        var services = _serviceProvider.GetServices<IInterfaceTwo>();

        Assert.Equal(2, services.Count());
        Assert.Contains(services, x => x.GetType() == typeof(ServiceOne));
        Assert.Contains(services, x => x.GetType() == typeof(ServiceTwo));
    }
}
