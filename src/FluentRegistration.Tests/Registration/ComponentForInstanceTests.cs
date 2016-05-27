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
    public class ComponentForInstanceTests
    {

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void CanRegister()
        {
            var tested = new ServiceCollection();

            var simpleService = new SimpleService();
            tested.Register(Component
                .For<ISimpleService>()
                .Instance(simpleService));

            Assert.Equal(1, tested.Count);
            Assert.All(tested, service =>
            {
                Assert.Equal(typeof(ISimpleService), service.ServiceType);
                Assert.Same(simpleService, service.ImplementationInstance);
            });

        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void CanRegisterTwoInterfaces()
        {
            var tested = new ServiceCollection();

            var simpleService = new ServiceWithTwoInterfaces();
            tested.Register(Component
                .ForAllInterfaces()
                .Instance(simpleService));

            Assert.Equal(2, tested.Count);
            Assert.All(tested, service =>
            {
                Assert.Same(simpleService, service.ImplementationInstance);
            });

        }

    }

}
