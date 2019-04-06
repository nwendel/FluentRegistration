#region License
// Copyright (c) Niklas Wendel 2016-2019
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
using FluentRegistration.Tests.Classes;

namespace FluentRegistration.Tests.Options
{

    /// <summary>
    /// 
    /// </summary>
    public class MultipleRegistrationsBehaviorTests
    {

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void CanRegisterSameImplementedByTwice()
        {
            var tested = new ServiceCollection();

            tested.Register(c => c
                .For<ISimpleService>()
                .ImplementedBy<SimpleService>());
            tested.Register(c => c
                .For<ISimpleService>()
                .ImplementedBy<SimpleService>());

            Assert.Single(tested);
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
        public void CanRegisterSameImplementedByTwiceIgnore()
        {
            var tested = new ServiceCollection();
            tested.Configure(c => c.MultipleRegistrationsBehavior = MultipleRegistrationsBehavior.Ignore);

            tested.Register(c => c
                .For<ISimpleService>()
                .ImplementedBy<SimpleService>());
            tested.Register(c => c
                .For<ISimpleService>()
                .ImplementedBy<SimpleService>());

            Assert.Single(tested);
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
        public void CanRegisterSameImplementedByTwiceRegister()
        {
            var tested = new ServiceCollection();
            tested.Configure(c => c.MultipleRegistrationsBehavior = MultipleRegistrationsBehavior.Register);

            tested.Register(c => c
                .For<ISimpleService>()
                .ImplementedBy<SimpleService>());
            tested.Register(c => c
                .For<ISimpleService>()
                .ImplementedBy<SimpleService>());

            Assert.Equal(2, tested.Count);
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
        public void CanRegisterSameImplementedByTwiceThrow()
        {
            var tested = new ServiceCollection();
            tested.Configure(c => c.MultipleRegistrationsBehavior = MultipleRegistrationsBehavior.ThrowException);

            tested.Register(c => c
                .For<ISimpleService>()
                .ImplementedBy<SimpleService>());

            Assert.Throws<RegistrationException>(() =>
                tested.Register(c => c
                    .For<ISimpleService>()
                    .ImplementedBy<SimpleService>()));
        }

    }

}
