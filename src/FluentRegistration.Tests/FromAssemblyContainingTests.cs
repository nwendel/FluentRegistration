using System;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace FluentRegistration.Tests
{
    public class FromAssemblyContainingTests
    {
        [Fact]
        public void ThrowsOnRegisterNullType()
        {
            var tested = new ServiceCollection();

            Assert.Throws<ArgumentNullException>("type", () => tested.Register(r => r
                .FromAssemblyContaining(null)
                .WithServices.Self()));
        }

        [Fact]
        public void ThrowsOnInstallNullType()
        {
            var tested = new ServiceCollection();

            Assert.Throws<ArgumentNullException>("type", () => tested.Install(i => i.FromAssemblyContaining(null)));
        }
    }
}
