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
        /// <param name="serviceCollection"></param>
        public void Register(IServiceCollection serviceCollection)
        {
            if (serviceCollection.Any(x => x.ImplementationType == _implementedByType))
            {
                // Already registered
                var options = serviceCollection.GetAttachedValue(ServiceCollectionAttachedProperties.Options) ?? FluentRegistrationOptions.Default;
                switch (options.MultipleRegistrationsBehavior)
                {
                    case MultipleRegistrationsBehavior.Ignore:
                        return;
                    case MultipleRegistrationsBehavior.Register:
                        break;
                    case MultipleRegistrationsBehavior.ThrowException:
                        throw new RegistrationException(string.Format("Implementation of type {0} already registrered", _implementedByType.FullName));
                }
            }

            if (!_serviceTypes.Any())
            {
                // No interfaces found
                var options = serviceCollection.GetAttachedValue(ServiceCollectionAttachedProperties.Options) ?? FluentRegistrationOptions.Default;
                switch (options.RegistrationsWithoutServicesBehavior)
                {
                    case RegistrationsWithoutServicesBehavior.Ignore:
                        return;
                    case RegistrationsWithoutServicesBehavior.ThrowException:
                        throw new RegistrationException(string.Format("No services found for implementation of type {0}", _implementedByType.FullName));
                }
            }

            var serviceType = _serviceTypes.First();
            var serviceDescriptor = new ServiceDescriptor(serviceType, _implementedByType, _lifetimeSelector.Lifetime);
            serviceCollection.Add(serviceDescriptor);

            var otherServicesType = _serviceTypes.Skip(1).ToList();
            foreach (var otherService in otherServicesType)
            {
                var otherServiceDescriptor = new ServiceDescriptor(otherService, serviceProvider => serviceProvider.GetService(serviceType), _lifetimeSelector.Lifetime);
                serviceCollection.Add(otherServiceDescriptor);
            }
        }

        #endregion

    }

}
