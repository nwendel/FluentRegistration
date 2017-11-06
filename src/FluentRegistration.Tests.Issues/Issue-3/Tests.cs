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
using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using FluentRegistration.Tests.Issues.Issue_3.Classes;

namespace FluentRegistration.Tests.Issues.Issue_3
{

    /// <summary>
    /// 
    /// </summary>
    public class Tests
    {

        private readonly ServiceCollection _tested = new ServiceCollection();
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// 
        /// </summary>
        public Tests()
        {
            _tested.Register(r => r
                .FromThisAssembly()
                .Where(c => c.InSameNamespaceAs<IInterfaceOne>())
                .WithServices.AllInterfaces());
            _serviceProvider = _tested.BuildServiceProvider();
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void CanResolveOne()
        {
            var services = _serviceProvider.GetServices<IInterfaceOne>();

            Assert.Equal(2, services.Count());
            Assert.Contains(services, x => x.GetType() == typeof(ServiceOne));
            Assert.Contains(services, x => x.GetType() == typeof(ServiceTwo));
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void CanResolveTwo()
        {
            var services = _serviceProvider.GetServices<IInterfaceTwo>();

            Assert.Equal(2, services.Count());
            Assert.Contains(services, x => x.GetType() == typeof(ServiceOne));
            Assert.Contains(services, x => x.GetType() == typeof(ServiceTwo));
        }

    }

}
