using System;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace FluentRegistration.Tests;

public class InvalidInstallTests
{
    [Fact]
    public void ThrowsOnNullServiceCollection()
    {
        ServiceCollection tested = null;

        Assert.Throws<ArgumentNullException>("self", () => tested.Install(null));
    }

    [Fact]
    public void ThrowsOnNullInstallers()
    {
        var tested = new ServiceCollection();

        Assert.Throws<ArgumentNullException>("installationAction", () => tested.Install(null));
    }
}
