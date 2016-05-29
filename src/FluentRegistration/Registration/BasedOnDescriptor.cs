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
    public class BasedOnDescriptor : AbstractCriteriasDescriptor
    {

        #region Fields

        private readonly AbstractFromDescriptor _fromDescriptor;
        private readonly Type _basedOn;

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromDescriptor"></param>
        /// <param name="basedOn"></param>
        /// <param name="filters"></param>
        internal BasedOnDescriptor(AbstractFromDescriptor fromDescriptor, Type basedOn)
        {
            _fromDescriptor = fromDescriptor;
            _basedOn = basedOn;
        }

        #endregion

        #region Selected Types

        /// <summary>
        /// 
        /// </summary>
        protected internal override IEnumerable<Type> SelectedTypes
        {
            get
            {
                var types = _fromDescriptor.SelectedTypes;

                var basedOnInfo = _basedOn.GetTypeInfo();
                if (basedOnInfo.IsGenericTypeDefinition)
                {
                    if (basedOnInfo.IsInterface)
                    {
                        types = types.Where(type => IsBasedOnGenericInterface(type)).ToArray();
                    }
                    else
                    {
                        types = types.Where(type => IsBasedOnGenericClass(type)).ToArray();
                    }
                }
                else
                {
                    types = types.Where(type => basedOnInfo.IsAssignableFrom(type)).ToArray();
                }

                return types;
            }
        }

        #endregion

        #region Is Based on Generic Interface

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private bool IsBasedOnGenericInterface(Type type)
        {
            var typeInfo = type.GetTypeInfo();
            var interfaces = typeInfo.GetInterfaces();
            foreach (var @interface in interfaces)
            {
                var interfaceInfo = @interface.GetTypeInfo();
                if (interfaceInfo.IsGenericType && @interface.GetGenericTypeDefinition() == _basedOn)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region Is Based On Generic Class

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private bool IsBasedOnGenericClass(Type type)
        {
            while (type != null)
            {
                var typeInfo = type.GetTypeInfo();
                if (typeInfo.IsGenericType && typeInfo.GetGenericTypeDefinition() == _basedOn)
                {
                    return true;
                }
                type = typeInfo.BaseType;
            }
            return false;
        }

        #endregion

    }

}
