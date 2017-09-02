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
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Linq;

namespace FluentRegistration.Internal
{

    /// <summary>
    /// 
    /// </summary>
    public class ServiceTypeSelector :
        IServiceSelector,
        IWithService,
        IServiceLifetimeAware,
        IRegister
    {

        #region Fields

        private readonly AbstractTypeSelector _typeSelector;
        private Func<Type, IEnumerable<Type>> _serviceTypeSelector;
        private ILifetimeSelector _lifetimeSelector;

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeSelector"></param>
        public ServiceTypeSelector(AbstractTypeSelector typeSelector)
        {
            _typeSelector = typeSelector;
            _lifetimeSelector = new LifetimeSelector(this);
        }

        #endregion

        #region All Interfaces

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IWithService AllInterfaces()
        {
            _serviceTypeSelector = type => type.GetInterfaces();
            return this;
        }

        #endregion
        
        #region Default Interface

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IWithService DefaultInterface()
        {
            _serviceTypeSelector = type => type.GetInterfaces();
            return this;
        }

        #endregion

        #region Interface

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IWithService Interface<TService>()
        {
            _serviceTypeSelector = type => type.GetInterfaces();
            return this;
        }

        #endregion

        #region Lifetime

        /// <summary>
        /// 
        /// </summary>
        public ILifetimeSelector Lifetime => _lifetimeSelector;

        /// <summary>
        /// 
        /// </summary>
        public ServiceLifetime ServiceLifetime { get; set; }

        #endregion

        #region Register

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceCollection"></param>
        public void Register(IServiceCollection serviceCollection)
        {
            var filteredTypes = _typeSelector.FilteredTypes;

            foreach(var type in filteredTypes)
            {
                var serviceTypes = _serviceTypeSelector(type);

                var serviceType = serviceTypes.First();
                var serviceDescriptor = new ServiceDescriptor(serviceType, type, ServiceLifetime);
                serviceCollection.Add(serviceDescriptor);

                var otherServicesType = serviceTypes.Skip(1).ToList();
                foreach (var otherService in otherServicesType)
                {
                    var otherServiceDescriptor = new ServiceDescriptor(otherService, serviceProviders => serviceProviders.GetService(serviceType), ServiceLifetime);
                    serviceCollection.Add(otherServiceDescriptor);
                }
            }
        }

        #endregion

    }

}
