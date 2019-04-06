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
using System;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using FluentRegistration.Tests.Classes;

namespace FluentRegistration.Tests
{

    /// <summary>
    /// 
    /// </summary>
    public class InstanceTests
    {

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void ThrowsOnRegisterNullInstance()
        {
            var tested = new ServiceCollection();

            Assert.Throws<ArgumentNullException>("instance", () =>
                tested.Register(r => r
                    .Instance<SimpleService>(null)
                    .WithServices.AllInterfaces()));
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void CanRegister()
        {
            var tested = new ServiceCollection();
            var instance = new SimpleService();

            tested.Register(r => r
                .Instance(instance)
                .WithServices.AllInterfaces());

            Assert.Single(tested);
            Assert.All(tested, service =>
            {
                Assert.Equal(typeof(ISimpleService), service.ServiceType);
                Assert.Same(instance, service.ImplementationInstance);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void CanRegisterDeclared()
        {
            var tested = new ServiceCollection();
            var instance = new SimpleService();

            tested.Register(r => r
                .Instance<object>(instance)
                .WithServices.AllInterfaces());

            Assert.Single(tested);
            Assert.All(tested, service =>
            {
                Assert.Equal(typeof(ISimpleService), service.ServiceType);
                Assert.Same(instance, service.ImplementationInstance);
            });
        }

    }

}
