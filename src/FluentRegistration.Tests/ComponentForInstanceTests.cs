using System;
using FluentRegistration.Tests.Classes;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace FluentRegistration.Tests
{
    public class ComponentForInstanceTests
    {
        [Fact]
        public void CanRegister()
        {
            var tested = new ServiceCollection();

            var simpleService = new SimpleService();

            tested.Register(r => r
                .For<ISimpleService>()
                .Instance(simpleService));

            Assert.Single(tested);
            Assert.All(tested, service =>
            {
                Assert.Equal(typeof(ISimpleService), service.ServiceType);
                Assert.Same(simpleService, service.ImplementationInstance);
            });
        }

        [Fact]
        public void ThrowsOnRegisterNullInstance()
        {
            var tested = new ServiceCollection();

            Assert.Throws<ArgumentNullException>("instance", () =>
                tested.Register(r => r
                    .For<ISimpleService>()
                    .Instance(null)));
        }
    }
}
