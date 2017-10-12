﻿#region License
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
    public class Installation : IInstallation
    {

        #region Fields

        private IServiceInstaller[] _installers;

        #endregion

        #region From Assembly

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assembly"></param>
        public void FromAssembly(Assembly assembly)
        {
            var allTypes = assembly.GetTypes();
            var installers = allTypes
                .Where(x => typeof(IServiceInstaller).GetTypeInfo().IsAssignableFrom(x))
                .Select(x => Activator.CreateInstance(x))
                .Cast<IServiceInstaller>()
                .ToArray();
            _installers = installers;
        }

        #endregion

        #region From Assembly Containing

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void FromAssemblyContaining<T>()
        {
            var assembly = typeof(T).GetTypeInfo().Assembly;
            FromAssembly(assembly);
        }

        #endregion

        #region From This Assembly

        /// <summary>
        /// 
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void FromThisAssembly()
        {
            FromAssembly(Assembly.GetCallingAssembly());
        }

        #endregion

        #region Install

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceCollection"></param>
        public void Install(IServiceCollection serviceCollection)
        {
            foreach(var installer in _installers)
            {
                installer.Install(serviceCollection);
            }
        }

        #endregion

    }

}
