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

using FluentRegistration.Options;
using FluentRegistration.Tests.Classes;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace FluentRegistration.Tests.Options
{

    /// <summary>
    /// 
    /// </summary>
    public class RegistrationsWithoutServicesBehaviourTests
    {

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void CanRegisterNoInterfaces()
        {
            var tested = new ServiceCollection();

            tested.Register(r => r
                .FromAssemblyContaining<RegistrationsWithoutServicesBehaviourTests>()
                .Where(c => c.ImplementationType == typeof(NoInterfaceService))
                .WithServices.DefaultInterface());

            Assert.Equal(0, tested.Count);
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void CanIgnoreNoServiesRegistration()
        {
            var tested = new ServiceCollection();
            tested.Configure(o => o.RegistrationsWithoutServicesBehavior = RegistrationsWithoutServicesBehavior.Ignore);

            tested.Register(r => r
                .FromAssemblyContaining<RegistrationsWithoutServicesBehaviourTests>()
                .Where(c => c.ImplementationType == typeof(NoInterfaceService))
                .WithServices.DefaultInterface());

            Assert.Equal(0, tested.Count);
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void ThrowsOnNoServiesRegistration()
        {
            var tested = new ServiceCollection();
            tested.Configure(o => o.RegistrationsWithoutServicesBehavior = RegistrationsWithoutServicesBehavior.ThrowException);

            Assert.Throws<RegistrationException>(() =>
                tested.Register(r => r
                    .FromAssemblyContaining<RegistrationsWithoutServicesBehaviourTests>()
                    .Where(c => c.ImplementationType == typeof(NoInterfaceService))
                    .WithServices.DefaultInterface()));
        }

    }

}
