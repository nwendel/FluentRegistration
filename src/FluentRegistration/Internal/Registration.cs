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
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

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
        public IImplementationSelector<TService> For<TService>()
        {
            var registration = new ComponentRegistration<TService>();
            _register = registration;
            return registration;
        }

        #endregion

        #region From AssemblyContaining

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ITypeSelector FromAssemblyContaining<T>()
        {
            var assembly = typeof(T).GetTypeInfo().Assembly;
            var registration = new AssemblyTypeSelector(assembly);
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
