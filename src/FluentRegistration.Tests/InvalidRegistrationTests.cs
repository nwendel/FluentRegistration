﻿#region License
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
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using FluentRegistration.Internal;
using FluentRegistration.Tests.TestClasses;

namespace FluentRegistration.Tests
{

    /// <summary>
    /// 
    /// </summary>
    public class InvalidRegistrationTests
    {

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void ThrowsOnNullRegistrations()
        {
            var tested = new ServiceCollection();

            Assert.Throws<ArgumentNullException>("registrationAction",
                () => tested.Register((Func<IRegistration, ICompleteRegistration>)null));
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void ThrowsOnNullServiceCollection()
        {
            ServiceCollection tested = null;

            Assert.Throws<ArgumentNullException>("self",
                () => tested.Register(r => r
                    .For<ISimpleService>()
                    .ImplementedBy<SimpleService>()));
        }

    }

}
