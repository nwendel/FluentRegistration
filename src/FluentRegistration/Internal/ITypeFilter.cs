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
    public interface ITypeFilter : 
        IFluentInterface
    {

        #region Assignable To

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        bool AssignableTo(Type type);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool AssignableTo<T>();

        #endregion

        #region In Namespace

        /// <summary>
        /// 
        /// </summary>
        /// <param name="namespace"></param>
        /// <returns></returns>
        bool InNamespace(string @namespace);

        #endregion

        #region In Same Namespace As

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        bool InSameNamespaceAs(Type type);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool InSameNamespaceAs<T>();

        #endregion

        #region In This Namespace

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool InThisNamespace();

        #endregion

        #region Implementation Type

        /// <summary>
        /// 
        /// </summary>
        Type ImplementationType { get; }

        #endregion

    }

}
