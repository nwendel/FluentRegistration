using System;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace FluentRegistration.Tests.Options;

public class InvalidConfigurationTests
{
    [Fact]
    public void ThrowsOnNullServiceCollection()
    {
        ServiceCollection tested = null;

        Assert.Throws<ArgumentNullException>("self", () => tested.Configure(o => { }));
    }

    [Fact]
    public void ThrowsOnNullOptionsAction()
    {
        var tested = new ServiceCollection();

        Assert.Throws<ArgumentNullException>("optionsAction", () => tested.Configure(null));
    }
}
