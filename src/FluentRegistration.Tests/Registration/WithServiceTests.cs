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
using FluentRegistration.Registration;
using FluentRegistration.Tests.TestClasses;

namespace FluentRegistration.Tests.Registration
{

    /// <summary>
    /// 
    /// </summary>
    public class WithServiceTests
    {

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void CanRegisterWithServiceSelf()
        {
            var tested = new ServiceCollection();

            tested.Register(AllClasses
                .FromAssemblyContaining<AllClassesBasedOnTests>()
                .BasedOn<SimpleService>()
                .WithService.Self());

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

            tested.Register(AllClasses
                    .FromAssemblyContaining<AllClassesInSameNamespaceTests>()
                    .BasedOn<ServiceWithDefaultInterface>()
                    .WithService.DefaultInterface());

            Assert.Equal(1, tested.Count);
            Assert.All(tested, service =>
            {
                Assert.Equal(typeof(IServiceWithDefaultInterface), service.ServiceType);
                Assert.Equal(typeof(ServiceWithDefaultInterface), service.ImplementationType);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void CanRegisterWithSpecificInterface()
        {
            var tested = new ServiceCollection();

            tested.Register(AllClasses
                    .FromAssemblyContaining<AllClassesInSameNamespaceTests>()
                    .BasedOn<SimpleService>()
                    .WithService.Interface<ISimpleService>());

            Assert.Equal(1, tested.Count);
            Assert.All(tested, service =>
            {
                Assert.Equal(typeof(ISimpleService), service.ServiceType);
                Assert.Equal(typeof(SimpleService), service.ImplementationType);
            });
        }

    }

}
