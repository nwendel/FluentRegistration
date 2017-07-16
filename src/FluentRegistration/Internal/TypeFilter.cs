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

namespace FluentRegistration.Internal
{

    /// <summary>
    /// 
    /// </summary>
    public class TypeFilter : ITypeFilter
    {

        #region Assignable To

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool AssignableTo<T>()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region In Same Namespace As

        /// <summary>
        /// 
        /// </summary>
        /// <param name="namespace"></param>
        /// <returns></returns>
        public Predicate<Type> InNamespace(string @namespace)
        {
            return type => type.Namespace == @namespace;
        }

        #endregion

        #region Is In Same Namespace As

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public Predicate<Type> InSameNamespaceAs(Type type)
        {
            return InNamespace(type.Namespace);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Predicate<Type> InSameNamespaceAs<T>()
        {
            return InSameNamespaceAs(typeof(T));
        }

        #endregion

    }

}
