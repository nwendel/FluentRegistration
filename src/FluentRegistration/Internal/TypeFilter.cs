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
            if(type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

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
                var candidateTypeInfo = candidateType.GetTypeInfo();
                if (candidateTypeInfo.IsGenericType && candidateTypeInfo.GetGenericTypeDefinition() == type)
                {
                    return true;
                }
                candidateType = candidateTypeInfo.BaseType;
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
            return InNamespace(@namespace, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="namespace"></param>
        /// <param name="includeSubNamespaces"></param>
        /// <returns></returns>
        public bool InNamespace(string @namespace, bool includeSubNamespaces)
        {
            if (string.IsNullOrWhiteSpace(@namespace))
            {
                throw new ArgumentNullException(nameof(@namespace));
            }

            if (ImplementationType.Namespace == @namespace)
            {
                return true;
            }
            if (includeSubNamespaces)
            {
                return ImplementationType.Namespace != null &&
                       ImplementationType.Namespace.StartsWith(@namespace + ".");
            }

            return false;
        }

        #endregion

        #region In Same Namespace As

        /// <summary>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool InSameNamespaceAs(Type type)
        {
            return InSameNamespaceAs(type, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="includeSubNamespaces"></param>
        /// <returns></returns>
        public bool InSameNamespaceAs(Type type, bool includeSubNamespaces)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return InNamespace(type.Namespace, includeSubNamespaces);
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool InSameNamespaceAs<T>()
        {
            return InSameNamespaceAs<T>(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="includeSubNamespaces"></param>
        /// <returns></returns>
        public bool InSameNamespaceAs<T>(bool includeSubNamespaces)
        {
            return InSameNamespaceAs(typeof(T), includeSubNamespaces);
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
            return InThisNamespaceCore(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="includeSubNamespaces"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public bool InThisNamespace(bool includeSubNamespaces)
        {

            return InThisNamespaceCore(includeSubNamespaces);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="includeSubNamespaces"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public bool InThisNamespaceCore(bool includeSubNamespaces)
        {
            var stackTrace = new StackTrace();
            var stackFrame = stackTrace.GetFrame(2);
            var method = stackFrame.GetMethod();
            var declaringType = method.DeclaringType;

            if (declaringType == null)
            {
                throw new RegistrationException($"Unable to determine declaring type for method {method.Name}");
            }

            return InNamespace(declaringType.Namespace, includeSubNamespaces);
        }

        #endregion

        #region Implementation Type

        /// <summary>
        /// </summary>
        public Type ImplementationType { get; }

        #endregion

    }

}
