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
using FluentRegistration.Tests.Classes.AnotherNamespace;

namespace FluentRegistration.Tests
{

    /// <summary>
    /// 
    /// </summary>
    public class AllClassesInSameNamespaceTests
    {

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void CanRegisterWhere()
        {
            var tested = new ServiceCollection();

            tested.Register(r => r
                .FromAssemblyContaining<AllClassesInSameNamespaceTests>()
                .Where(c => c.InSameNamespaceAs<ServiceInAnotherNamespace>())
                .WithServices.AllInterfaces());

            Assert.Single(tested);
            Assert.All(tested, service =>
            {
                Assert.Equal(typeof(IServiceInAnotherNamespace), service.ServiceType);
                Assert.Equal(typeof(ServiceInAnotherNamespace), service.ImplementationType);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void CanRegisterExcept()
        {
            var tested = new ServiceCollection();

            tested.Register(r => r
                .FromAssemblyContaining<AllClassesInSameNamespaceTests>()
                .Where(c => c.InSameNamespaceAs<ServiceInAnotherNamespace>())
                .Except(c => c.ImplementationType == typeof(ServiceInAnotherNamespace))
                .WithServices.AllInterfaces());

            Assert.Empty(tested);
        }
        
    }

}


