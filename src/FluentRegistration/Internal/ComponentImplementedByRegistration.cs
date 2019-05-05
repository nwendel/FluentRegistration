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
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using AttachedProperties;
using FluentRegistration.Options;
using System.Collections.Generic;

namespace FluentRegistration.Internal
{

    /// <summary>
    /// 
    /// </summary>
    public class ComponentImplementedByRegistration<TService, TImplementation> :
        ILifetime,
        IRegister
        where TImplementation : TService
    {

        #region Fields

        private readonly IEnumerable<Type> _serviceTypes;
        private readonly Type _implementedByType;
        private readonly LifetimeSelector _lifetimeSelector;

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceTypes"></param>
        public ComponentImplementedByRegistration(IEnumerable<Type> serviceTypes)
            : this(serviceTypes, typeof(TImplementation), new LifetimeSelector())
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceTypes"></param>
        /// <param name="implementedByType"></param>
        /// <param name="lifetimeSelector"></param>
        public ComponentImplementedByRegistration(IEnumerable<Type> serviceTypes, Type implementedByType, LifetimeSelector lifetimeSelector)
        {
            _serviceTypes = serviceTypes;
            _implementedByType = implementedByType;
            _lifetimeSelector = lifetimeSelector;
        }

        #endregion

        #region Lifetime

        /// <summary>
        /// 
        /// </summary>
        public ILifetimeSelector Lifetime => _lifetimeSelector;

        #endregion

        #region Register

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void Register(IServiceCollection services)
        {
            if (services.Any(x => x.ImplementationType == _implementedByType))
            {
                // Already registered
                var options = services.GetAttachedValue(ServiceCollectionAttachedProperties.Options) ?? FluentRegistrationOptions.Default;
                switch (options.MultipleRegistrationsBehavior)
                {
                    case MultipleRegistrationsBehavior.Ignore:
                        return;
                    case MultipleRegistrationsBehavior.Register:
                        break;
                    case MultipleRegistrationsBehavior.ThrowException:
                        throw new RegistrationException($"Implementation of type {_implementedByType.FullName} already registered");
                }
            }

            if (!_serviceTypes.Any())
            {
                // No interfaces found
                var options = services.GetAttachedValue(ServiceCollectionAttachedProperties.Options) ?? FluentRegistrationOptions.Default;
                switch (options.RegistrationsWithoutServicesBehavior)
                {
                    case RegistrationsWithoutServicesBehavior.Ignore:
                        return;
                    case RegistrationsWithoutServicesBehavior.ThrowException:
                        throw new RegistrationException($"No services found for implementation of type {_implementedByType.FullName}");
                }
            }

            if(_serviceTypes.Count() == 1)
            {
                var serviceType = _serviceTypes.First();
                var serviceDescriptor = new ServiceDescriptor(serviceType, _implementedByType, _lifetimeSelector.Lifetime);
                services.Add(serviceDescriptor);
            }
            else
            {
                // TODO: Workaround to solve problem with registering multiple implementation types under same shared interface
                // TODO: Since they should be resolved to same instance in case of singleton or scoped lifestyle

                var selfServiceDescriptor = new ServiceDescriptor(_implementedByType, _implementedByType, _lifetimeSelector.Lifetime);
                services.Add(selfServiceDescriptor);

                foreach (var serviceType in _serviceTypes)
                {
                    if (serviceType == _implementedByType)
                    {
                        // Already registered with self above
                        continue;
                    }

                    var serviceDescriptor = new ServiceDescriptor(serviceType, serviceProvider => serviceProvider.GetService(_implementedByType), _lifetimeSelector.Lifetime);
                    services.Add(serviceDescriptor);
                }
            }
        }

        #endregion

    }

}
