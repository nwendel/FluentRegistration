using System;
using FluentRegistration.Tests.Classes;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace FluentRegistration.Tests
{
    public class FactoryTests
    {
        [Fact]
        public void ThrowsOnRegisterUsingFactoryNullFactoryMethod()
        {
            var tested = new ServiceCollection();

            Assert.Throws<ArgumentNullException>("factoryMethod", () =>
                tested.Register(c => c
                .For<ISimpleService>()
                .UsingFactory<SimpleServiceServiceFactory>(null)));
        }

        [Fact]
        public void CanRegisterUsingFactory()
        {
            var tested = new ServiceCollection();

            tested.Register(c => c
                .For<SimpleServiceServiceFactory>()
                .ImplementedBy<SimpleServiceServiceFactory>());
            tested.Register(c => c
                .For<ISimpleService>()
                .UsingFactory<SimpleServiceServiceFactory>(f => f.CreateSimpleService()));
            var serviceProvider = tested.BuildServiceProvider();
            var service = serviceProvider.GetService<ISimpleService>();

            Assert.Same(SimpleServiceServiceFactory.SimpleService, service);
        }

        [Fact]
        public void CanRegisterUsingFactoryMethod()
        {
            var tested = new ServiceCollection();

            var simpleService = new SimpleService();
            tested.Register(c => c
                    .For<ISimpleService>()
                    .UsingFactoryMethod(() => simpleService));
            var serviceProvider = tested.BuildServiceProvider();
            var service = serviceProvider.GetService<ISimpleService>();

            Assert.Same(simpleService, service);
        }

        [Fact]
        public void ThrowsOnUsingFactoryMethod()
        {
            var tested = new ServiceCollection();

            Assert.Throws<ArgumentNullException>("factoryMethod", () =>
                tested.Register(c => c
                    .For<ISimpleService>()
                    .UsingFactoryMethod((Func<ISimpleService>)null)));
        }

        [Fact]
        public void ThrowsOnUsingFactoryMethodWithServiceProvider()
        {
            var tested = new ServiceCollection();

            Assert.Throws<ArgumentNullException>("factoryMethod", () =>
                tested.Register(c => c
                    .For<ISimpleService>()
                    .UsingFactoryMethod((Func<IServiceProvider, ISimpleService>)null)));
        }
    }
}
