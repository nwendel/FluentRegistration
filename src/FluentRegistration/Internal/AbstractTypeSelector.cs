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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace FluentRegistration.Internal
{

    /// <summary>
    /// 
    /// </summary>
    public abstract class AbstractTypeSelector :
        ITypeSelector,
        IRegister
    {

        #region Fields

        private readonly List<Func<ITypeFilter, bool>> _wherePredicates = new List<Func<ITypeFilter, bool>>();
        private readonly List<Func<ITypeFilter, bool>> _exceptPredicates = new List<Func<ITypeFilter, bool>>();
        private ServiceTypeSelector _serviceTypeSelector;

        #endregion

        #region Types

        /// <summary>
        /// 
        /// </summary>
        /// 
        protected abstract IEnumerable<Type> Types { get; }

        #endregion

        #region Where

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public ITypeSelector Where(Func<ITypeFilter, bool> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            _wherePredicates.Add(predicate);
            return this;
        }

        #endregion

        #region Except

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public ITypeSelector Except(Func<ITypeFilter, bool> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            _exceptPredicates.Add(predicate);
            return this;
        }

        #endregion

        #region Filtered Types

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<Type> FilteredTypes
        {
            get
            {
                return Types
                    .Where(type => 
                    {
                        var typeInfo = type.GetTypeInfo();
                        return typeInfo.IsClass && !typeInfo.IsAbstract;
                    })
                    .Where(type => _wherePredicates.Count == 0 || _wherePredicates.Any(filter => filter(new TypeFilter(type))))
                    .Where(type => _exceptPredicates.Count == 0 || !_exceptPredicates.Any(filter => filter(new TypeFilter(type))));
            }
        }

        #endregion

        #region With Services

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
        /// <param name="serviceCollection"></param>
        public void Register(IServiceCollection serviceCollection)
        {
            var filteredTypes = FilteredTypes;

            foreach (var type in filteredTypes)
            {
                var serviceTypes = _serviceTypeSelector.GetServicesFor(type);
                var serviceLifetimeSelector = _serviceTypeSelector.GetLifetimeSelector();

                var componentRegistration = new ComponentImplementedByRegistration<object, object>(serviceTypes, type, serviceLifetimeSelector);
                componentRegistration.Register(serviceCollection);
            }
        }

        #endregion

    }

}
