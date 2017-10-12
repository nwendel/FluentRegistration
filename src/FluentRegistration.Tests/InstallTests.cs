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

using FluentRegistration.Tests.Classes;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace FluentRegistration.Tests
{

    /// <summary>
    /// 
    /// </summary>
    public class InstallTests
    {

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void CanInstall()
        {
            var tested = new ServiceCollection();

            tested.Install<SimpleServiceInstaller>();

            Assert.Equal(1, tested.Count);
            Assert.All(tested, service =>
            {
                Assert.Equal(ServiceLifetime.Singleton, service.Lifetime);
                Assert.Equal(typeof(ISimpleService), service.ServiceType);
                Assert.Equal(typeof(SimpleService), service.ImplementationType);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void CanInstallFromAssemblyContaining()
        {
            var tested = new ServiceCollection();

            tested.Install(i => i.FromAssemblyContaining<SimpleServiceInstaller>());

            Assert.Equal(1, tested.Count);
            Assert.All(tested, service =>
            {
                Assert.Equal(ServiceLifetime.Singleton, service.Lifetime);
                Assert.Equal(typeof(ISimpleService), service.ServiceType);
                Assert.Equal(typeof(SimpleService), service.ImplementationType);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void CanInstallFromThisAssembly()
        {
            var tested = new ServiceCollection();

            tested.Install(i => i.FromThisAssembly());

            Assert.Equal(1, tested.Count);
            Assert.All(tested, service =>
            {
                Assert.Equal(ServiceLifetime.Singleton, service.Lifetime);
                Assert.Equal(typeof(ISimpleService), service.ServiceType);
                Assert.Equal(typeof(SimpleService), service.ImplementationType);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void ThrowsOnInstallFromAssemblyNullAssembly()
        {
            var tested = new ServiceCollection();

            Assert.Throws<ArgumentNullException>("assembly", () =>
                tested.Install(i => i.FromAssembly(null)));
        }

    }

}
