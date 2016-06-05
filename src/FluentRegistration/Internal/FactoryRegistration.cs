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
using Microsoft.Extensions.DependencyInjection;

namespace FluentRegistration.Internal
{

    /// <summary>
    /// 
    /// </summary>
    public class FactoryRegistration : IRegistration, IFluentInterface
    {

        #region Fields

        private readonly Type[] _services;
        private readonly Func<IServiceProvider, object> _factoryMethod;

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="factoryMethod"></param>
        /// <param name="servicesSelector"></param>
        /// <param name="services"></param>
        internal FactoryRegistration(Func<IServiceProvider, object> factoryMethod, params Type[] services)
        {
            _factoryMethod = factoryMethod;
            _services = services;
        }

        #endregion

        #region Register

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceCollection"></param>
        void IRegistration.Register(IServiceCollection serviceCollection)
        {
            foreach (var service in _services)
            {
                var serviceDescriptor = new ServiceDescriptor(service, serviceProvider => _factoryMethod(serviceProvider), ServiceLifetime.Transient);
                serviceCollection.Add(serviceDescriptor);
            }
        }

        #endregion

    }

}