﻿using FluentRegistration.Tests.Classes;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace FluentRegistration.Tests
{
    public class LifestimeTests
    {
        [Fact]
        public void CanRegisterNoLifetime()
        {
            var tested = new ServiceCollection();

            tested.Register(r => r
                .ImplementedBy<SimpleService>()
                .WithServices.AllInterfaces());

            Assert.Single(tested);
            Assert.All(tested, service =>
            {
                Assert.Equal(ServiceLifetime.Singleton, service.Lifetime);
            });
        }

        [Fact]
        public void CanRegisterSingletonLifetime()
        {
            var tested = new ServiceCollection();

            tested.Register(r => r
                .ImplementedBy<SimpleService>()
                .WithServices.AllInterfaces()
                .Lifetime.Singleton());

            Assert.Single(tested);
            Assert.All(tested, service =>
            {
                Assert.Equal(ServiceLifetime.Singleton, service.Lifetime);
            });
        }

        [Fact]
        public void CanRegisterScopedLifetime()
        {
            var tested = new ServiceCollection();

            tested.Register(r => r
                .ImplementedBy<SimpleService>()
                .WithServices.AllInterfaces()
                .Lifetime.Scoped());

            Assert.Single(tested);
            Assert.All(tested, service =>
            {
                Assert.Equal(ServiceLifetime.Scoped, service.Lifetime);
            });
        }

        [Fact]
        public void CanRegisterTransientLifetime()
        {
            var tested = new ServiceCollection();

            tested.Register(r => r
                .ImplementedBy<SimpleService>()
                .WithServices.AllInterfaces()
                .Lifetime.Transient());

            Assert.Single(tested);
            Assert.All(tested, service =>
            {
                Assert.Equal(ServiceLifetime.Transient, service.Lifetime);
            });
        }

        [Fact]
        public void CanRegisterSingletonLifetimeMultipleServices()
        {
            var services = new ServiceCollection();
            services.Register(r => r
                .FromAssemblyContaining<SimpleService>()
                .Where(c => c.ImplementationType == typeof(SimpleService))
                .WithServices
                    .Self()
                    .DefaultInterface());
            var tested = services.BuildServiceProvider();

            var serviceOne = tested.GetRequiredService<ISimpleService>();
            var serviceTwo = tested.GetRequiredService<SimpleService>();
            Assert.Same(serviceOne, serviceTwo);
        }
    }
}
