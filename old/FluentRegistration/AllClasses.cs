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
using System.Reflection;
using System.Runtime.CompilerServices;
using FluentRegistration.Internal;

namespace FluentRegistration
{

    /// <summary>
    /// 
    /// </summary>
    public static class AllClasses
    {

        #region From Assembly

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static FromAssemblyDescriptor FromAssembly(Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            var fromAssemblyDescriptor = new FromAssemblyDescriptor(assembly, IsNonAbstractClass);
            return fromAssemblyDescriptor;
        }

        #endregion

        #region From Assembly Containing

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static FromAssemblyDescriptor FromAssemblyContaining<T>()
        {
            return FromAssembly(typeof(T).GetTypeInfo().Assembly);
        }

        #endregion

        #region From This Assembly

#if !NETSTANDARD1_6

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static FromAssemblyDescriptor FromThisAssembly()
        {
            return FromAssembly(Assembly.GetCallingAssembly());
        }

#endif

        #endregion

        #region From Types

        /// <summary>
        /// 
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        public static FromTypesDescriptor FromTypes(IEnumerable<Type> types)
        {
            return new FromTypesDescriptor(types, IsNonAbstractClass);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        public static FromTypesDescriptor FromTypes(params Type[] types)
        {
            return new FromTypesDescriptor(types, IsNonAbstractClass);
        }

        #endregion

        #region Is Non Abstract Class

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        internal static Predicate<Type> IsNonAbstractClass => type =>
        {
            var typeInfo = type.GetTypeInfo();
            return typeInfo.IsClass && !typeInfo.IsAbstract;
        };

        #endregion

    }

}
