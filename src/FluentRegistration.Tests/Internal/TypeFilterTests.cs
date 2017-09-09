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

using System;
using Xunit;
using FluentRegistration.Internal;
using FluentRegistration.Tests.TestClasses;

namespace FluentRegistration.Tests.Internal
{

    /// <summary>
    /// 
    /// </summary>
    public class TypeFilterTests
    {

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void ThrowsOnInNamespaceNull()
        {
            var tested = new TypeFilter(typeof(SimpleService));

            Assert.Throws<ArgumentNullException>(() => tested.InNamespace(null));
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void CanAssignableTo()
        {
            var tested = new TypeFilter(typeof(SimpleService));

            Assert.True(tested.AssignableTo<ISimpleService>());
        }

    }

}