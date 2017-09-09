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

namespace FluentRegistration.Internal
{

    /// <summary>
    /// 
    /// </summary>
    public class ComponentImplementedByRegistration<TService, TImplementation> :
        ILifetime,
        ICompleteRegistration,
        IRegister
        where TImplementation : TService
    {

        #region Fields

        private Type _serviceType => typeof(TService);
        private Type _implementedByType => typeof(TImplementation);
        private readonly LifetimeSelector _lifetimeSelector = new LifetimeSelector();

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

            var serviceDescriptor = new ServiceDescriptor(_serviceType, _implementedByType, _lifetimeSelector.Lifetime);
            serviceCollection.Add(serviceDescriptor);
        }

        #endregion

    }

}
