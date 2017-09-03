#region License
// Copyright (c) Niklas Wendel 2016-2017
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
using FluentRegistration.Tests.TestClasses;

namespace FluentRegistration.Tests
{

    /// <summary>
    /// 
    /// </summary>
    public class WithServicesTests
    {

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void CanRegisterWithServicesSelf()
        {
            var tested = new ServiceCollection();

            tested.Register(r => r
                .FromAssemblyContaining<SimpleService>()
                .Where(c => c.ImplementationType == typeof(SimpleService))
                .WithServices.Self());

            Assert.Equal(1, tested.Count);
            Assert.All(tested, service =>
            {
                Assert.Equal(typeof(SimpleService), service.ServiceType);
                Assert.Equal(typeof(SimpleService), service.ImplementationType);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void CanRegisterWithDefaultInterface()
        {
            var tested = new ServiceCollection();

            tested.Register(r => r
                .FromAssemblyContaining<SimpleService>()
                .Where(c => c.ImplementationType == typeof(SimpleService))
                .WithServices.DefaultInterface());

            Assert.Equal(1, tested.Count);
            Assert.All(tested, service =>
            {
                Assert.Equal(typeof(ISimpleService), service.ServiceType);
                Assert.Equal(typeof(SimpleService), service.ImplementationType);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void CanRegisterWithSpecificInterface()
        {
            var tested = new ServiceCollection();

            tested.Register(r => r
                .FromAssemblyContaining<SimpleService>()
                .Where(c => c.ImplementationType == typeof(SimpleService))
                    .WithServices.Interface<ISimpleService>());

            Assert.Equal(1, tested.Count);
            Assert.All(tested, service =>
            {
                Assert.Equal(typeof(ISimpleService), service.ServiceType);
                Assert.Equal(typeof(SimpleService), service.ImplementationType);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void CanRegisterWithServicesDefaultInterfaceSelf()
        {
            var tested = new ServiceCollection();

            tested.Register(r => r
                .FromAssemblyContaining<SimpleService>()
                .Where(c => c.ImplementationType == typeof(SimpleService))
                .WithServices
                    .Self()
                    .DefaultInterface());

            Assert.Equal(2, tested.Count);
            Assert.Contains(tested, x => x.ServiceType == typeof(ISimpleService));
            Assert.Contains(tested, x => x.ServiceType == typeof(SimpleService));
        }

    }

}
