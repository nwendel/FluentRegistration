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
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace FluentRegistration.Internal
{

    /// <summary>
    /// 
    /// </summary>
    public class ServiceTypeSelector :
        IWithService
    {

        #region Fields

        private readonly List<Func<Type, IEnumerable<Type>>> _serviceTypeSelectors = new List<Func<Type, IEnumerable<Type>>>();
        private readonly LifetimeSelector _lifetimeSelector = new LifetimeSelector();

        #endregion

        #region All Interfaces

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IWithService AllInterfaces()
        {
            _serviceTypeSelectors.Add(type => type.GetTypeInfo().GetInterfaces());
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
            _serviceTypeSelectors.Add(type =>
            {
                var typeInfo = type.GetTypeInfo();
                var interfaces = typeInfo.GetInterfaces();
                var defaultInterfaces = interfaces.Where(i =>
                {
                    var name = i.Name;
                    if (name.Length > 1 && name[0] == 'I' && char.IsUpper(name[1]))
                    {
                        name = name.Substring(1);
                    }

                    return type.Name.Contains(name);
                });
                return defaultInterfaces;
            });
            
            return this;
        }

        #endregion

        #region Service

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IWithService Service<TService>()
        {
            _serviceTypeSelectors.Add(type => type.GetTypeInfo().GetInterfaces());
            return this;
        }

        #endregion

        #region Self

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IWithService Self()
        {
            _serviceTypeSelectors.Add(type => new[] { type });
            return this;
        }

        #endregion

        #region Get Services For

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IEnumerable<Type> GetServicesFor(Type type)
        {
            return _serviceTypeSelectors.SelectMany(selector => selector(type));
        }

        #endregion

        #region Lifetime

        /// <summary>
        /// 
        /// </summary>
        public ILifetimeSelector Lifetime => _lifetimeSelector;

        #endregion

        #region Get Lifetime Selector

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public LifetimeSelector GetLifetimeSelector() => _lifetimeSelector;

        #endregion

    }

}
