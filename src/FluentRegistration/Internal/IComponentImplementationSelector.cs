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

namespace FluentRegistration.Internal
{

    /// <summary>
    /// 
    /// </summary>
    public interface IComponentImplementationSelector<TService> : 
        IFluentInterface
    {

        #region Implemented By

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TImplementation"></typeparam>
        ILifetime ImplementedBy<TImplementation>()
            where TImplementation : TService;

        #endregion

        #region Instance

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        IValidRegistration Instance(TService instance);

        #endregion

        #region Using Factory

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TFactory"></typeparam>
        /// <param name="factoryMethod"></param>
        /// <returns></returns>
        IValidRegistration UsingFactory<TFactory>(Func<TFactory, TService> factoryMethod)
            where TFactory : class;

        #endregion

        #region Using Factory Method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="factoryMethod"></param>
        /// <returns></returns>
        IValidRegistration UsingFactoryMethod(Func<TService> factoryMethod);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="factoryMethod"></param>
        /// <returns></returns>
        IValidRegistration UsingFactoryMethod(Func<IServiceProvider, TService> factoryMethod);

        #endregion

    }

}
