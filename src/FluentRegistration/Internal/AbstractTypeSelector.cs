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
    public abstract class AbstractTypeSelector :
        ITypeSelector,
        IWithServiceInitial,
        //IServiceSelector,
        //ILifetime,
        //IServiceLifetimeAware,
        IRegister
    {

        #region Fields

        private IRegister _register;
        private List<Predicate<Type>> _wherePredicates = new List<Predicate<Type>>();
        private List<Predicate<Type>> _exceptPredicates = new List<Predicate<Type>>();
        //private readonly ILifetimeSelector _lifetimeSelector;

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        protected AbstractTypeSelector()
        {
            //_lifetimeSelector = new LifetimeSelector(this);
        }

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
        public ITypeSelector Where(Func<ITypeFilter, Predicate<Type>> predicate)
        {
            var typeFilter = new TypeFilter();
            var wherePredicate = predicate.Invoke(typeFilter);
            _wherePredicates.Add(wherePredicate);
            return this;
        }

        #endregion

        #region Except

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public ITypeSelector Except(Func<ITypeFilter, Predicate<Type>> predicate)
        {
            var typeFilter = new TypeFilter();
            var exceptPredicate = predicate.Invoke(typeFilter);
            _exceptPredicates.Add(exceptPredicate);
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
                    .Where(type => _wherePredicates.Count == 0 || _wherePredicates.Any(filter => filter(type)))
                    .Where(type => _wherePredicates.Count == 0 || !_exceptPredicates.Any(filter => filter(type)));
            }
        }

        #endregion

        #region With Service

        /// <summary>
        /// 
        /// </summary>
        public IServiceSelector WithService
        {
            get
            {
                var serviceTypeSelector = new ServiceTypeSelector(this);
                _register = serviceTypeSelector;
                return serviceTypeSelector;
            }
        }

        #endregion

        #region All Interfaces

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //public IWithService AllInterfaces()
        //{
        //    return this;
        //}

        #endregion

        #region Lifetime

        /// <summary>
        /// 
        /// </summary>
        //public ILifetimeSelector Lifetime => _lifetimeSelector;

        #endregion

        #region Service Lifetime

        /// <summary>
        /// 
        /// </summary>
        //public ServiceLifetime ServiceLifetime { get; set; }

        #endregion

        #region Register

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceCollection"></param>
        public void Register(IServiceCollection serviceCollection)
        {
            _register.Register(serviceCollection);


            //var types = Types.Where(x =>
            //{
            //    var typeInfo = x.GetTypeInfo();
            //    return typeInfo.IsClass && !typeInfo.IsAbstract;
            //}).ToList();

            //foreach(var type in types)
            //{
            //    if (_wherePredicates.Count != 0 && !_wherePredicates.Any(filter => filter(type)))
            //    {
            //        continue;
            //    }
            //    if (_exceptPredicates.Count != 0 && _exceptPredicates.Any(filter => filter(type)))
            //    {
            //        continue;
            //    }

            //    var serviceTypes = type.GetInterfaces();

            //    var serviceType = serviceTypes.First();
            //    var serviceDescriptor = new ServiceDescriptor(serviceType, type, ServiceLifetime);
            //    serviceCollection.Add(serviceDescriptor);

            //    var otherServicesType = serviceTypes.Skip(1).ToList();
            //    foreach (var otherService in otherServicesType)
            //    {
            //        var otherServiceDescriptor = new ServiceDescriptor(otherService, serviceProviders => serviceProviders.GetService(serviceType), ServiceLifetime);
            //        serviceCollection.Add(otherServiceDescriptor);
            //    }




            //}
        }

        #endregion

    }

}
