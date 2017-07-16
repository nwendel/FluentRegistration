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
using System;
using Microsoft.Extensions.DependencyInjection;

namespace FluentRegistration.Internal
{

    /// <summary>
    /// 
    /// </summary>
    public class ComponentRegistration<TService> : 
        IImplementationSelector<TService>,
        ILifetime,
        IServiceLifetimeAware,
        IRegister
    {

        #region Fields

        private readonly IServiceCollection _serviceCollection;
        private readonly ILifetimeSelector _lifetimeSelector;
        private Type _serviceType => typeof(TService);
        private Type _implementedByType;
        private TService _instance;

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceCollection"></param>
        public ComponentRegistration()
        {
            _lifetimeSelector = new LifetimeSelector(this);
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
            _implementedByType = typeof(TImplementation);
            return this;
        }

        #endregion

        #region Lifetime

        /// <summary>
        /// 
        /// </summary>
        public ILifetimeSelector Lifetime => _lifetimeSelector;

        #endregion

        #region Service Lifetime

        /// <summary>
        /// 
        /// </summary>
        public ServiceLifetime ServiceLifetime { get ; set; }

        #endregion

        #region Instance

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        public void Instance(TService instance)
        {
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
            if (_instance != null)
            {
                var serviceDescriptor = new ServiceDescriptor(_serviceType, _instance);
                serviceCollection.Add(serviceDescriptor);
            }
            else
            {
                var serviceDescriptor = new ServiceDescriptor(_serviceType, _implementedByType, ServiceLifetime);
                serviceCollection.Add(serviceDescriptor);
            }
        }

        #endregion

    }

}
