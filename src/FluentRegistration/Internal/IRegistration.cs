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
    public interface IRegistration :
        IFluentInterface
    {

        #region For

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        IComponentImplementationSelector<TService> For<TService>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        IComponentImplementationSelector<object> For(Type type);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        IComponentImplementationSelector<object> For(params Type[] types);

        #endregion

        #region From Assembly

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        ITypeSelector FromAssembly(Assembly assembly);

        #endregion

        #region From Assembly Containing

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        ITypeSelector FromAssemblyContaining(Type type);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        ITypeSelector FromAssemblyContaining<T>();

        #endregion

        #region From This Assembly

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ITypeSelector FromThisAssembly();

        #endregion

        #region Implemented By

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IWithServicesInitial ImplementedBy<T>();

        #endregion

        #region Instance

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <returns></returns>
        IWithServicesInitial Instance<T>(T instance);

        #endregion

    }

}
