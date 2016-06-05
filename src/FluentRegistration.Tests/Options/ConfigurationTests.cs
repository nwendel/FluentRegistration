#region License
// Copyright (c) Niklas Wendel 2016
// 
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at 
// 
// http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License.
#endregion
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using FluentRegistration.Options;
using FluentRegistration.Tests.TestClasses;

namespace FluentRegistration.Tests.Options
{

    /// <summary>
    /// 
    /// </summary>
    public class ConfigurationTests
    {

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void CanIgnoreDuplicateRegistration()
        {
            var tested = new ServiceCollection();
            tested.ConfigureFluentRegistration(o =>
            {
                o.MultipleRegistrationsBehavior = MultipleRegistrationsBehavior.Ignore;
            });

            tested.Register(Component
                .For<ISimpleService>()
                .ImplementedBy<SimpleService>());
            tested.Register(Component
                .For<ISimpleService>()
                .ImplementedBy<SimpleService>());

            Assert.Equal(1, tested.Count);
            Assert.All(tested, service =>
            {
                Assert.Equal(ServiceLifetime.Singleton, service.Lifetime);
                Assert.Equal(typeof(ISimpleService), service.ServiceType);
                Assert.Equal(typeof(SimpleService), service.ImplementationType);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void ThrowsOnDuplicateRegistration()
        {
            var tested = new ServiceCollection();
            tested.ConfigureFluentRegistration(o =>
            {
                o.MultipleRegistrationsBehavior = MultipleRegistrationsBehavior.ThrowException;
            });

            tested.Register(Component
                .For<ISimpleService>()
                .ImplementedBy<SimpleService>());

            Assert.Throws<RegistrationException>(
                () => tested.Register(Component
                    .For<ISimpleService>()
                    .ImplementedBy<SimpleService>()));
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void CanIgnoreNoServiesRegistration()
        {
            var tested = new ServiceCollection();
            tested.ConfigureFluentRegistration(o =>
            {
                o.RegistrationsWithoutServicesBehavior = RegistrationsWithoutServicesBehavior.Ignore;
            });

            tested.Register(AllClasses
                .FromAssemblyContaining<ConfigurationTests>()
                .BasedOn<ServiceWithoutInterface>()
                .WithService.DefaultInterface());

            Assert.Equal(0, tested.Count);
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void ThrowsOnNoServiesRegistration()
        {
            var tested = new ServiceCollection();
            tested.ConfigureFluentRegistration(o =>
            {
                o.RegistrationsWithoutServicesBehavior = RegistrationsWithoutServicesBehavior.ThrowException;
            });

            Assert.Throws<RegistrationException>(
            () => tested.Register(AllClasses
                .FromAssemblyContaining<ConfigurationTests>()
                .BasedOn<ServiceWithoutInterface>()
                .WithService.DefaultInterface()));
        }

    }

}
