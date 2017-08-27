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
    public class ServiceTypeSelector :
        IServiceSelector,
        IRegister
    {

        #region Fields

        private readonly AbstractTypeSelector _typeSelector;

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeSelector"></param>
        public ServiceTypeSelector(AbstractTypeSelector typeSelector)
        {
            _typeSelector = typeSelector;
        }

        #endregion

        public IWithService AllInterfaces()
        {
            throw new NotImplementedException();
        }

        #region Register

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceCollection"></param>
        public void Register(IServiceCollection serviceCollection)
        {
            var filteredTypes = _typeSelector.FilteredTypes;



            throw new NotImplementedException();
        }

        #endregion

    }

}
