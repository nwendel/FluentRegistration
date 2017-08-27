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
    public class ComponentImplementedByRegistration<TService, TImplementation> :
        ILifetime,
        IServiceLifetimeAware,
        ICompleteRegistration,
        IRegister
        where TImplementation : TService
    {

        #region Fields

        private Type _serviceType => typeof(TService);
        private Type _implementedByType => typeof(TImplementation);
        private readonly ILifetimeSelector _lifetimeSelector;

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        public ComponentImplementedByRegistration()
        {
            _lifetimeSelector = new LifetimeSelector(this);
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

        #region Register

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceCollection"></param>
        public void Register(IServiceCollection serviceCollection)
        {
            var serviceDescriptor = new ServiceDescriptor(_serviceType, _implementedByType, ServiceLifetime);
            serviceCollection.Add(serviceDescriptor);
        }

        #endregion

    }

}
