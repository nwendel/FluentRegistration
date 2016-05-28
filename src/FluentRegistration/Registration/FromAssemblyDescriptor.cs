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
using System;
using System.Collections.Generic;
using System.Reflection;

namespace FluentRegistration.Registration
{

    /// <summary>
    /// 
    /// </summary>
    public class FromAssemblyDescriptor : AbstractFromDescriptor
    {

        #region Fields

        private Assembly _assembly;

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="filter"></param>
        internal FromAssemblyDescriptor(Assembly assembly, Predicate<Type> filter) : base(filter)
        {
            _assembly = assembly;
        }

        #endregion

        #region Selected Types

        /// <summary>
        /// 
        /// </summary>
        protected internal override IEnumerable<Type> SelectedTypes
        {
            get
            {
                var types = _assembly.GetTypes();
                return types;
            }
        }

        #endregion

    }

}
