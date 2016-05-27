using FluentRegistration.Registration;
using FluentRegistration.Tests.TestClasses;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace FluentRegistration.Tests.Registration
{

    /// <summary>
    /// 
    /// </summary>
    public class InvalidRegistrationTests
    {

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void ThrowsOnNullServiceCollection()
        {
            ServiceCollection tested = null;

            Assert.Throws<ArgumentNullException>("self",
                () => tested.Register(Component.For<ISimpleService>()));
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void ThrowsOnNullRegistrations()
        {
            var tested = new ServiceCollection();

            Assert.Throws<ArgumentNullException>("registrations",
                () => tested.Register(null));
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void ThrowsOnComponentNoImplementation()
        {
            var tested = new ServiceCollection();

            Assert.Throws<RegistrationException>(
                () => tested.Register(Component.For<ISimpleService>()));
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void ThrowsOnNoWithService()
        {
            var tested = new ServiceCollection();

            Assert.Throws<RegistrationException>(
                () => tested.Register(AllClasses
                    .FromAssemblyContaining<InvalidRegistrationTests>()
                    .BasedOn<ISimpleService>()));
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void ThrowsOnMultipleLifetimes()
        {
            var tested = new ServiceCollection();

            Assert.Throws<RegistrationException>(
                () => tested.Register(AllClasses
                    .FromAssemblyContaining<InvalidRegistrationTests>()
                    .BasedOn<ISimpleService>()
                    .WithService.AllInterfaces()
                    .Lifetime.Singleton
                    .Lifetime.Singleton));
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void ThrowsOnMultipleWithService()
        {
            var tested = new ServiceCollection();

            Assert.Throws<RegistrationException>(
                () => tested.Register(AllClasses
                    .FromAssemblyContaining<InvalidRegistrationTests>()
                    .BasedOn<ISimpleService>()
                    .WithService.AllInterfaces()
                    .WithService.AllInterfaces()));
        }

    }

}
