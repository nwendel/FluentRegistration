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
using System;
using Microsoft.Extensions.DependencyInjection;

namespace FluentRegistration.Registration
{

    /// <summary>
    /// 
    /// </summary>
    public class InstanceRegistration : IRegistration, IFluentInterface
    {

        #region Fields

        private readonly Type[] _services;
        private readonly ServicesSelector _servicesSelector;
        private readonly object _instance;

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="servicesSelector"></param>
        /// <param name="services"></param>
        internal InstanceRegistration(object instance, ServicesSelector servicesSelector, params Type[] services)
        {
            _instance = instance;
            _servicesSelector = servicesSelector;
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
            var services = _services ?? _servicesSelector(_instance.GetType());
            foreach (var service in services)
            {
                var serviceDescriptor = new ServiceDescriptor(service, _instance);
                serviceCollection.Add(serviceDescriptor);
            }
        }

        #endregion

    }

}
