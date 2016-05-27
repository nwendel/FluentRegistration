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
using System.Reflection;

namespace FluentRegistration.Registration
{

    /// <summary>
    /// 
    /// </summary>
    public class ServicesDescriptor
    {

        #region Fields

        private readonly AbstractCriteriasDescriptor _criteriasDescriptor;
        private ServicesSelector _servicesSelector;
        private bool _isCalled;

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="criteriasDescriptor"></param>
        internal ServicesDescriptor(AbstractCriteriasDescriptor criteriasDescriptor)
        {
            _criteriasDescriptor = criteriasDescriptor;
        }

        #endregion

        #region Interface

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public AbstractCriteriasDescriptor Interface<T>()
        {
            EnsureSingleCall();
            _servicesSelector = type => new[] { typeof(T) };
            return _criteriasDescriptor;
        }

        #endregion

        #region All Interfaces

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public AbstractCriteriasDescriptor AllInterfaces()
        {
            EnsureSingleCall();
            _servicesSelector = type => type.GetTypeInfo().GetInterfaces();
            return _criteriasDescriptor;
        }

        #endregion

        #region First Interface

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public AbstractCriteriasDescriptor FirstInterface()
        {
            EnsureSingleCall();
            _servicesSelector = type =>
            {
                var typeInfo = type.GetTypeInfo();
                var firstInterface = typeInfo.GetInterfaces().FirstOrDefault();
                if (firstInterface == null)
                {
                    return null;
                }
                return new[] { firstInterface };
            };
            return _criteriasDescriptor;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public AbstractCriteriasDescriptor DefaultInterface()
        {
            EnsureSingleCall();
            _servicesSelector = type =>
            {
                var typeInfo = type.GetTypeInfo();
                var interfaces = typeInfo.GetInterfaces();
                var defaultInterfaces = interfaces.Where(@interface =>
                {
                    var name = @interface.Name;
                    if(name.Length > 1 && name[0] == 'I' && char.IsUpper(name[1]))
                    {
                        name = name.Substring(1);
                    }

                    return type.Name.Contains(name);
                });
                return defaultInterfaces;
            };
            return _criteriasDescriptor;
        }

        #endregion

        #region Self

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public AbstractCriteriasDescriptor Self()
        {
            EnsureSingleCall();
            _servicesSelector = x => new[] { x };
            return _criteriasDescriptor;
        }

        #endregion

        #region Ensure Single Call

        /// <summary>
        /// 
        /// </summary>
        private void EnsureSingleCall()
        {
            if (_isCalled)
            {
                throw new RegistrationException("WithService can only be set once per registration");
            }
            _isCalled = true;
        }

        #endregion

        #region Get Services

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        internal IEnumerable<Type> GetServices(Type type)
        {
            if(_servicesSelector == null)
            {
                throw new RegistrationException("Cannot register components without WithService");
            }

            var services = _servicesSelector(type);
            return services;
        }

        #endregion

    }

}
