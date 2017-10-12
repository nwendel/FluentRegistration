﻿#region License
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
    public class FromAssemblyTests
    {

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void CanRegister()
        {
            var tested = new ServiceCollection();

            tested.Register(r => r
                .FromThisAssembly()
                .Where(c => c.InSameNamespaceAs<ServiceInAnotherNamespace>())
                .WithServices.AllInterfaces());

            Assert.Equal(1, tested.Count);
            Assert.All(tested, service =>
            {
                Assert.Equal(typeof(IServiceInAnotherNamespace), service.ServiceType);
                Assert.Equal(typeof(ServiceInAnotherNamespace), service.ImplementationType);
            });
        }

    }

}

