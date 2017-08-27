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
using Microsoft.Extensions.DependencyInjection;

namespace FluentRegistration.Internal
{

    /// <summary>
    /// 
    /// </summary>
    public class LifetimeSelector : ILifetimeSelector, ICompleteRegistration
    {

        #region Fields

        private readonly IServiceLifetimeAware _serviceLifetimeAware;

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceLifetimeAware"></param>
        public LifetimeSelector(IServiceLifetimeAware serviceLifetimeAware)
        {
            _serviceLifetimeAware = serviceLifetimeAware;
        }

        #endregion

        #region Singleton

        /// <summary>
        /// 
        /// </summary>
        public ICompleteRegistration Singleton()
        {
            _serviceLifetimeAware.ServiceLifetime = ServiceLifetime.Singleton;
            return this;
            //return new CompleteRegistration();
        }

        #endregion

        #region Scoped

        /// <summary>
        /// 
        /// </summary>
        public ICompleteRegistration Scoped()
        {
            _serviceLifetimeAware.ServiceLifetime = ServiceLifetime.Scoped;
            return this;
            //return new CompleteRegistration();
        }

        #endregion

        #region Transient

        /// <summary>
        /// 
        /// </summary>
        public ICompleteRegistration Transient()
        {
            _serviceLifetimeAware.ServiceLifetime = ServiceLifetime.Transient;
            return this;
            //return new CompleteRegistration();
        }

        #endregion

    }

}
