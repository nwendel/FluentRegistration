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
using Microsoft.Extensions.DependencyInjection;

namespace FluentRegistration.Registration
{

    /// <summary>
    /// 
    /// </summary>
    public class LifetimeDescriptor<T>
        where T : IRegistration
    {

        #region Fields

        private readonly T _descriptor;
        private ServiceLifetime _lifetime = ServiceLifetime.Singleton;
        private bool _isCalled;

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registration"></param>
        internal LifetimeDescriptor(T descriptor)
        {
            _descriptor = descriptor;
        }

        #endregion

        #region Singleton

        /// <summary>
        /// 
        /// </summary>
        public T Singleton
        {
            get
            {
                EnsureSingleCall();
                _lifetime = ServiceLifetime.Singleton;
                return _descriptor;
            }
        }

        #endregion

        #region Scoped

        /// <summary>
        /// 
        /// </summary>
        public T Scoped
        {
            get
            {
                EnsureSingleCall();
                _lifetime = ServiceLifetime.Scoped;
                return _descriptor;
            }
        }

        #endregion

        #region Transient

        /// <summary>
        /// 
        /// </summary>
        public T Transient
        {
            get
            {
                EnsureSingleCall();
                _lifetime = ServiceLifetime.Transient;
                return _descriptor;
            }
        }

        #endregion

        #region Ensure Single Call

        /// <summary>
        /// 
        /// </summary>
        private void EnsureSingleCall()
        {
            if(_isCalled)
            {
                throw new RegistrationException("Lifetime can only be set once per registration");
            }
            _isCalled = true;
        }

        #endregion

        #region Get Services

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        internal ServiceLifetime GetLifetime()
        {
            return _lifetime;
        }

        #endregion

    }

}
