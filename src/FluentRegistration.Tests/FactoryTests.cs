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
using FluentRegistration.Tests.Classes;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace FluentRegistration.Tests
{

    /// <summary>
    /// 
    /// </summary>
    public class FactoryTests
    {

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void ThrowsOnRegisterUsingFactoryNullFactoryMethod()
        {
            var tested = new ServiceCollection();

            Assert.Throws<ArgumentNullException>("factoryMethod", () => 
                tested.Register(c => c
                .For<ISimpleService>()
                .UsingFactory<SimpleServiceServiceFactory>(null)));
        }

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void ThrowsOnUsingFactoryMethod()
        {
            var tested = new ServiceCollection();

            Assert.Throws<ArgumentNullException>("factoryMethod", () =>
                tested.Register(c => c
                    .For<ISimpleService>()
                    .UsingFactoryMethod((Func<ISimpleService>)null)));
        }

        /// <summary>
        /// 
        /// </summary>
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
