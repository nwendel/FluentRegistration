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
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace FluentRegistration.Internal
{

    /// <summary>
    /// </summary>
    public class TypeFilter : ITypeFilter
    {

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        public TypeFilter(Type type)
        {
            ImplementationType = type;
        }

        #endregion

        #region Assignable To

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool AssignableTo(Type type)
        {
            var typeInfo = type.GetTypeInfo();
            if(typeInfo.IsGenericTypeDefinition)
            {
                if(typeInfo.IsInterface)
                {
                    return AssignableToGenericInterface(typeInfo);
                }
                return AssignableToGenericClass(typeInfo);

            }
            return type.GetTypeInfo().IsAssignableFrom(ImplementationType);
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool AssignableTo<T>()
        {
            return AssignableTo(typeof(T));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private bool AssignableToGenericInterface(Type type)
        {
            var interfaces = ImplementationType.GetInterfaces();
            foreach (var @interface in interfaces)
            {
                var interfaceTypeInfo = @interface.GetTypeInfo();
                if (interfaceTypeInfo.IsGenericType && @interface.GetGenericTypeDefinition() == type)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private bool AssignableToGenericClass(Type type)
        {
            var candidateType = ImplementationType;
            while (candidateType != null)
            {
                var canidateTypeInfo = candidateType.GetTypeInfo();
                if (canidateTypeInfo.IsGenericType && canidateTypeInfo.GetGenericTypeDefinition() == type)
                {
                    return true;
                }
                candidateType = canidateTypeInfo.BaseType;
            }
            return false;
        }

        #endregion

        #region In Namespace

        /// <summary>
        /// </summary>
        /// <param name="namespace"></param>
        /// <returns></returns>
        public bool InNamespace(string @namespace)
        {
            if(string.IsNullOrWhiteSpace(@namespace))
            {
                throw new ArgumentNullException(nameof(@namespace));
            }

            return ImplementationType.Namespace == @namespace;
        }

        #endregion

        #region In Same Namespace As

        /// <summary>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool InSameNamespaceAs(Type type)
        {
            return InNamespace(type.Namespace);
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool InSameNamespaceAs<T>()
        {
            return InSameNamespaceAs(typeof(T));
        }

        #endregion

        #region In This Namespace

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public bool InThisNamespace()
        {
            var stackTrace = new StackTrace();
            var stackFrame = stackTrace.GetFrame(1);
            var @namespace = stackFrame.GetMethod().DeclaringType.Namespace;

            return InNamespace(@namespace);
        }

        #endregion

        #region Implementation Type

        /// <summary>
        /// </summary>
        public Type ImplementationType { get; }

        #endregion

    }

}
