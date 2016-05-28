#region License
// Copyright (c) Niklas Wendel 2016
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
using Microsoft.Extensions.DependencyInjection;

namespace FluentRegistration.Registration
{

    /// <summary>
    /// 
    /// </summary>
    public abstract class AbstractCriteriasDescriptor : IRegistration, IFluentInterface
    {

        #region Fields

        private readonly Predicate<Type> _filter;
        private readonly ICollection<Predicate<Type>> _whereFilters = new List<Predicate<Type>>();
        private readonly ICollection<Predicate<Type>> _exceptFilters = new List<Predicate<Type>>();
        private readonly ServicesDescriptor _servicesDescriptor;
        private readonly LifetimeDescriptor<AbstractCriteriasDescriptor> _lifetimeDescriptor;

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        protected AbstractCriteriasDescriptor()
        {
            _servicesDescriptor = new ServicesDescriptor(this);
            _lifetimeDescriptor = new LifetimeDescriptor<AbstractCriteriasDescriptor>(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        protected AbstractCriteriasDescriptor(Predicate<Type> filter) : this()
        {
            _filter = filter;
        }

        #endregion

        #region Where

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public AbstractCriteriasDescriptor Where(Predicate<Type> filter)
        {
            _whereFilters.Add(filter);
            return this;
        }

        #endregion

        #region Except

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public AbstractCriteriasDescriptor Except(Predicate<Type> filter)
        {
            _exceptFilters.Add(filter);
            return this;
        }

        #endregion

        #region With Service

        /// <summary>
        /// 
        /// </summary>
        public ServicesDescriptor WithService => _servicesDescriptor;

        #endregion

        #region Lifetime

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public LifetimeDescriptor<AbstractCriteriasDescriptor> Lifetime => _lifetimeDescriptor;

        #endregion

        #region Selected Types

        /// <summary>
        /// 
        /// </summary>
        protected internal abstract IEnumerable<Type> SelectedTypes { get; }

        #endregion

        #region Register

        private Dictionary<ServiceLifetime, Action<ComponentRegistration<object>>> _lifetimeActionLookup = new Dictionary<ServiceLifetime, Action<ComponentRegistration<object>>>
        {
            { ServiceLifetime.Singleton, x => { var _ = x.Lifetime.Singleton; } },
            { ServiceLifetime.Scoped, x => { var _ = x.Lifetime.Scoped; } },
            { ServiceLifetime.Transient, x => { var _ =  x.Lifetime.Transient; } }
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceCollection"></param>
        void IRegistration.Register(IServiceCollection serviceCollection)
        {
            var lifetimeAction = _lifetimeActionLookup[_lifetimeDescriptor.GetLifetime()];
            foreach (var type in SelectedTypes)
            {
                if(_filter != null && !_filter(type))
                {
                    continue;
                }
                if(_whereFilters.Count != 0 && !_whereFilters.Any(filter => filter(type)))
                {
                    continue;
                }
                if (_exceptFilters.Count != 0 && _exceptFilters.Any(filter => !filter(type)))
                {
                    continue;
                }

                var services = _servicesDescriptor.GetServices(type).ToArray();

                var registration = Component
                    .For(services)
                    .ImplementedBy(type);
                lifetimeAction(registration);

                serviceCollection.Register(registration);
            }
        }

        #endregion

    }

}
