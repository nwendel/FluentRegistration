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
using System.Reflection;

namespace FluentRegistration.Registration
{

    /// <summary>
    /// 
    /// </summary>
    public class Component
    {

        #region For

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ComponentRegistration<T> For<T>()
        {
            return new ComponentRegistration<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ComponentRegistration For(Type type)
        {
            return new ComponentRegistration(type);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ComponentRegistration For(params Type[] types)
        {
            return new ComponentRegistration(types);
        }

        #endregion

        #region Implemented By

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static AbstractCriteriasDescriptor ImplementedBy<T>()
        {
            return AllClasses
                .FromAssemblyContaining<T>()
                .Where(x => x == typeof(T));
        }

        #endregion

        #region Is In Namespace

        /// <summary>
        /// 
        /// </summary>
        /// <param name="namespace"></param>
        /// <returns></returns>
        public static Predicate<Type> IsInNamespace(string @namespace)
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
        public static Predicate<Type> IsInSameNamespaceAs(Type type)
        {
            return IsInNamespace(type.Namespace);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Predicate<Type> IsInSameNamespaceAs<T>()
        {
            return IsInSameNamespaceAs(typeof(T));
        }

        #endregion

        #region Has Attribute

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributeType"></param>
        /// <returns></returns>
        public static Predicate<Type> HasAttribute<TAttribute>()
            where TAttribute : Attribute
        {
            return type => type.GetTypeInfo().IsDefined(typeof(TAttribute));
        }

        #endregion

    }

}
