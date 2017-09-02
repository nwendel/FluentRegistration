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
using System.Reflection;

namespace FluentRegistration.Internal
{

    /// <summary>
    /// 
    /// </summary>
    public class TypeFilter : ITypeFilter
    {

        #region Fields

        private readonly Type _type;

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        public TypeFilter(Type type)
        {
            _type = type;
        }

        #endregion

        #region Assignable To

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool AssignableTo<T>()
        {
            return typeof(T).IsAssignableFrom(_type);
        }

        #endregion

        #region Is In Namespace

        /// <summary>
        /// 
        /// </summary>
        /// <param name="namespace"></param>
        /// <returns></returns>
        public bool InNamespace(string @namespace)
        {
            if(string.IsNullOrWhiteSpace(@namespace))
            {
                throw new ArgumentNullException(nameof(@namespace));
            }

            return _type.Namespace == @namespace;
        }

        #endregion

        #region In Same Namespace As

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool InSameNamespaceAs(Type type)
        {
            return InNamespace(type.Namespace);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool InSameNamespaceAs<T>()
        {
            return InSameNamespaceAs(typeof(T));
        }

        #endregion

        #region Implementation Type

        /// <summary>
        /// 
        /// </summary>
        public Type ImplementationType
        {
            get { return _type; }
        }

        #endregion

    }

}
