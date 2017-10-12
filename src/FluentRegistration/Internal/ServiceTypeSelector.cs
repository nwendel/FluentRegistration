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
        IWithService,
        IRegister
    {

        #region Fields

        private readonly AbstractTypeSelector _typeSelector;
        private readonly List<Func<Type, IEnumerable<Type>>> _serviceTypeSelector = new List<Func<Type, IEnumerable<Type>>>();
        private readonly LifetimeSelector _lifetimeSelector = new LifetimeSelector();

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeSelector"></param>
        public ServiceTypeSelector(AbstractTypeSelector typeSelector)
        {
            _typeSelector = typeSelector;
        }

        #endregion

        #region All Interfaces

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IWithService AllInterfaces()
        {
            _serviceTypeSelector.Add(type => type.GetTypeInfo().GetInterfaces());
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
            _serviceTypeSelector.Add(type =>
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

        #region Interface

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IWithService Interface<TService>()
        {
            _serviceTypeSelector.Add(type => type.GetTypeInfo().GetInterfaces());
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
            _serviceTypeSelector.Add(type => new[] { type });
            return this;
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
            var filteredTypes = _typeSelector.FilteredTypes;

            foreach(var type in filteredTypes)
            {
                var serviceTypes = _serviceTypeSelector
                    .SelectMany(selector => selector(type))
                    .ToArray();

                var componentRegistration = new ComponentImplementedByRegistration<object, object>(serviceTypes, type, _lifetimeSelector);
                componentRegistration.Register(serviceCollection);
            }
        }

        #endregion

    }

}
