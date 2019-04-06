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
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace FluentRegistration.Internal
{

    /// <summary>
    /// 
    /// </summary>
    public class ComponentInstanceRegistration :
        IValidRegistration,
        IRegister
    {

        #region Fields

        private readonly IEnumerable<Type> _serviceTypes;
        private readonly object _instance;

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceTypes"></param>
        /// <param name="instance"></param>
        public ComponentInstanceRegistration(IEnumerable<Type> serviceTypes, object instance)
        {
            _serviceTypes = serviceTypes;
            _instance = instance;
        }

        #endregion

        #region Register

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceCollection"></param>
        public void Register(IServiceCollection serviceCollection)
        {
            foreach (var serviceType in _serviceTypes)
            {
                var serviceDescriptor = new ServiceDescriptor(serviceType, _instance);
                serviceCollection.Add(serviceDescriptor);
            }
        }

        #endregion

    }

}
