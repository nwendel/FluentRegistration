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
    public class ComponentForAllInterfacesTests : AbstractTests
    {

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void CanRegisterTwoInterfacesSingletonLifetime()
        {
            var serviceProvider = BuildServiceProvider(x =>
            {
                x.Register(Component
                    .ForAllInterfaces()
                    .ImplementedBy<ServiceWithTwoInterfaces>()
                    .Lifetime.Singleton);
            });

            var serviceOne = serviceProvider.GetService<IServiceOne>();
            var serviceTwo = serviceProvider.GetService<IServiceTwo>();
            Assert.Same(serviceOne, serviceTwo);
        }

    }

}
