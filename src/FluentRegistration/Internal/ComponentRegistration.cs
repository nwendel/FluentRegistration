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
    public class ComponentRegistration<TService> : 
        IComponentImplementationSelector<TService>,
        IRegister
    {

        #region Fields

        private IRegister _register;
        private readonly Type[] _serviceTypes;

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceTypes"></param>
        public ComponentRegistration(Type[] serviceTypes)
        {
            _serviceTypes = serviceTypes;
        }

        #endregion

        #region Implemented By

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TImplementation"></typeparam>
        /// <returns></returns>
        public ILifetime ImplementedBy<TImplementation>()
            where TImplementation : TService
        {
            var implementedByRegistration = new ComponentImplementedByRegistration<TService, TImplementation>(_serviceTypes);
            _register = implementedByRegistration;
            return implementedByRegistration;
        }

        #endregion

        #region Instance

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        public IValidRegistration Instance(TService instance)
        {
            if(instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            var instanceRegistration = new ComponentInstanceRegistration<TService>(_serviceTypes, instance);
            _register = instanceRegistration;
            return instanceRegistration;
        }

        #endregion

        #region Register

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceCollection"></param>
        public void Register(IServiceCollection serviceCollection)
        {
            _register.Register(serviceCollection);
        }

        #endregion

    }

}
