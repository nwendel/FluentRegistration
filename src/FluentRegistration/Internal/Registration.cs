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
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;

namespace FluentRegistration.Internal
{

    /// <summary>
    /// 
    /// </summary>
    public class Registration : IRegistration
    {

        #region Fields

        private IRegister _register;

        #endregion

        #region For

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public IComponentImplementationSelector<TService> For<TService>()
        {
            var registration = new ComponentRegistration<TService>(new[] { typeof(TService) });
            _register = registration;
            return registration;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IComponentImplementationSelector<object> For(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return For(new[] {type});
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        public IComponentImplementationSelector<object> For(params Type[] types)
        {
            if (!types.Any())
            {
                throw new ArgumentNullException(nameof(types));
            }

            var registration = new ComponentRegistration<object>(types);
            _register = registration;
            return registration;
        }

        #endregion

        #region From Assembly

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public ITypeSelector FromAssembly(Assembly assembly)
        {
            if(assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            var registration = new AssemblyTypeSelector(assembly);
            _register = registration;
            return registration;
        }

        #endregion

        #region From Assembly Containing

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ITypeSelector FromAssemblyContaining(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            var assembly = type.GetTypeInfo().Assembly;
            return FromAssembly(assembly);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ITypeSelector FromAssemblyContaining<T>()
        {
            return FromAssemblyContaining(typeof(T));
        }

        #endregion

        #region From This Assembly

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public ITypeSelector FromThisAssembly()
        {
            return FromAssembly(Assembly.GetCallingAssembly());
        }

        #endregion

        #region Implemented By

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IWithServicesInitial ImplementedBy<T>()
        {
            return FromAssemblyContaining<T>()
                .Where(c => c.ImplementationType == typeof(T));
        }

        #endregion

        #region Instance

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <returns></returns>
        public IWithServicesInitial Instance<T>(T instance)
        {
            var registration = new InstanceRegistration<T>(instance);
            _register = registration;
            return registration;
        }

        #endregion

        #region Register

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceCollection"></param>
        public void Register(IServiceCollection serviceCollection)
        {
            _register.Register(serviceCollection);
        }

        #endregion

    }

}
