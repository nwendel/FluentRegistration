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
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace FluentRegistration.Tests
{

    /// <summary>
    /// 
    /// </summary>
    public class FromAssemblyContainingTests
    {

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void ThrowsOnRegisterNullType()
        {
            var tested = new ServiceCollection();


            Assert.Throws<ArgumentNullException>("type", () => tested.Register(r => r
                .FromAssemblyContaining(null)
                .WithServices.Self()));
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void ThrowsOnInstallNullType()
        {
            var tested = new ServiceCollection();

            Assert.Throws<ArgumentNullException>("type", () => tested.Install(i => i.FromAssemblyContaining(null)));
        }

    }

}
