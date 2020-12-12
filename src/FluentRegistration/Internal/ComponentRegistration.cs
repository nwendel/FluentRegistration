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
using Microsoft.Extensions.DependencyInjection;

namespace FluentRegistration.Internal
{

    /// <summary>
    /// 
    /// </summary>
    public class ComponentRegistration<TService> : 
        IComponentImplementationSelector<TService>,
        IRegister
        where TService : notnull
    {

        #region Fields

        private IRegister? _register;
        private readonly Type[] _serviceTypes;

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceTypes"></param>
        public ComponentRegistration(Type[] serviceTypes)
        {
            _serviceTypes = serviceTypes;
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
            var implementedByRegistration = new ComponentImplementedByRegistration<TService, TImplementation>(_serviceTypes);
            _register = implementedByRegistration;
            return implementedByRegistration;
        }

        #endregion

        #region Instance

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        public IValidRegistration Instance(TService instance)
        {
            if(instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            var instanceRegistration = new ComponentInstanceRegistration(_serviceTypes, instance);
            _register = instanceRegistration;
            return instanceRegistration;
        }

        #endregion

        #region Using Factory

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TFactory"></typeparam>
        /// <param name="factoryMethod"></param>
        /// <returns></returns>
        public IValidRegistration UsingFactory<TFactory>(Func<TFactory, TService> factoryMethod)
            where TFactory : class
        {
            if (factoryMethod == null)
            {
                throw new ArgumentNullException(nameof(factoryMethod));
            }

            var factoryRegistration = new ComponentFactoryRegistration<TFactory, TService>(factoryMethod);
            _register = factoryRegistration;
            return factoryRegistration;
        }

        #endregion

        #region Using Factory Method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="factoryMethod"></param>
        /// <returns></returns>
        public IValidRegistration UsingFactoryMethod(Func<TService> factoryMethod)
        {
            if(factoryMethod == null)
            {
                throw new ArgumentNullException(nameof(factoryMethod));
            }

            return UsingFactoryMethod(serviceProvider => factoryMethod());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="factoryMethod"></param>
        /// <returns></returns>
        public IValidRegistration UsingFactoryMethod(Func<IServiceProvider, TService> factoryMethod)
        {
            if (factoryMethod == null)
            {
                throw new ArgumentNullException(nameof(factoryMethod));
            }

            var factoryMethodRegistration = new ComponentFactoryMethodRegistration<TService>(factoryMethod);
            _register = factoryMethodRegistration;
            return factoryMethodRegistration;
        }

        #endregion

        #region Register

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void Register(IServiceCollection services)
        {
            if (_register == null)
            {
                throw new InvalidOperationException("Register called without defining what to register via the fluent Api.");
            }

            _register.Register(services);
        }

        #endregion

    }

}
