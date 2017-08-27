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

namespace FluentRegistration.Internal
{

    /// <summary>
    /// 
    /// </summary>
    public class ComponentRegistration : IComponentRegistration, IRegister
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
            var register = new ComponentRegistration<TService>();
            _register = register;
            return register;
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


    /// <summary>
    /// 
    /// </summary>
    public class ComponentRegistration<TService> : 
        IComponentImplementationSelector<TService>,
        IRegister
    {

        #region Fields

        private IRegister _register;

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        public ComponentRegistration()
        {
        }

        #endregion

        #region Implemented By

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TImplementation"></typeparam>
        /// <returns></returns>
        public ILifetime ImplementedBy<TImplementation>()
            where TImplementation : TService
        {
            var implementedByRegistration = new ComponentImplementedByRegistration<TService, TImplementation>();
            _register = implementedByRegistration;
            return implementedByRegistration;
        }

        #endregion

        #region Instance

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        public ICompleteRegistration Instance(TService instance)
        {
            if(instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            var instanceRegistration = new ComponentInstanceRegistration<TService>(instance);
            _register = instanceRegistration;
            return instanceRegistration;
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
