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
using FluentRegistration.Registration;
using FluentRegistration.Tests.TestClasses;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Xunit;

namespace FluentRegistration.Tests.Registration
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
        public void CanRegisterUsingFactory()
        {
            var tested = new ServiceCollection();

            tested.Register(Component
                .For<IServiceFactory<ISimpleService>>()
                .ImplementedBy<SimpleServiceServiceFactory>());
            tested.Register(Component
                    .For<ISimpleService>()
                    .UsingFactory());
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
            tested.Register(Component
                    .For<ISimpleService>()
                    .UsingFactoryMethod(() => simpleService));
            var serviceProvider = tested.BuildServiceProvider();
            var service = serviceProvider.GetService<ISimpleService>();

            Assert.Same(simpleService, service);
        }

    }

}
