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

namespace FluentRegistration.Internal
{

    /// <summary>
    /// 
    /// </summary>
    public class ComponentFactoryRegistration<TService> :
        IValidRegistration,
        IRegister
    {

        #region Fields

        private readonly Func<IServiceProvider, TService> _factoryMethod;

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="factoryMethod"></param>
        public ComponentFactoryRegistration(Func<IServiceProvider, TService> factoryMethod)
        {
            _factoryMethod = factoryMethod;
        }

        #endregion

        #region Register

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceCollection"></param>
        public void Register(IServiceCollection serviceCollection)
        {
            var serviceDescriptor = new ServiceDescriptor(typeof(TService), serviceProvider => _factoryMethod(serviceProvider), ServiceLifetime.Transient);
            serviceCollection.Add(serviceDescriptor);
        }

        #endregion

    }

}