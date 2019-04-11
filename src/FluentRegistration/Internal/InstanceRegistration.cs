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
using Microsoft.Extensions.DependencyInjection;

namespace FluentRegistration.Internal
{

    /// <summary>
    /// 
    /// </summary>
    public class InstanceRegistration<T> :
        IWithServicesInitial, 
        IRegister
    {

        #region Fields

        private readonly T _instance;
        private ServiceTypeSelector _serviceTypeSelector;

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        public InstanceRegistration(T instance)
        {
            if(instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            _instance = instance;
        }

        #endregion

        #region With Service

        /// <summary>
        /// 
        /// </summary>
        public IServiceSelector WithServices
        {
            get
            {
                _serviceTypeSelector = new ServiceTypeSelector();
                return _serviceTypeSelector;
            }
        }

        #endregion

        #region Register

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void Register(IServiceCollection services)
        {
            var serviceTypes = _serviceTypeSelector.GetServicesFor(_instance.GetType());
            var componentRegistration = new ComponentInstanceRegistration(serviceTypes, _instance);
            componentRegistration.Register(services);
        }

        #endregion

    }

}
