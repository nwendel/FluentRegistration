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
using AttachedProperties;
using FluentRegistration.Internal;
using FluentRegistration.Options;

namespace FluentRegistration
{

    /// <summary>
    /// 
    /// </summary>
    public static class ServiceCollectionExtensions
    {

        #region Register

        /// <summary>
        /// 
        /// </summary>
        /// <param name="self"></param>
        /// <param name="registrationAction"></param>
        public static void Register(this IServiceCollection self, Func<IRegistration, IValidRegistration> registrationAction)
        {
            if (self == null)
            {
                throw new ArgumentNullException(nameof(self));
            }
            if (registrationAction == null)
            {
                throw new ArgumentNullException(nameof(registrationAction));
            }

            var registration = new Registration();
            registrationAction(registration);
            registration.Register(self);
        }

        #endregion

        #region Install

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TInstaller"></typeparam>
        /// <param name="self"></param>
        public static void Install<TInstaller>(this IServiceCollection self)
            where TInstaller : IServiceInstaller, new()
        {
            var installer = new TInstaller();
            installer.Install(self);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="self"></param>
        /// <param name="installationAction"></param>
        public static void Install(this IServiceCollection self, Action<IInstallation> installationAction)
        {
            if (self == null)
            {
                throw new ArgumentNullException(nameof(self));
            }
            if (installationAction == null)
            {
                throw new ArgumentNullException(nameof(installationAction));
            }

            var installation = new Installation();
            installationAction(installation);
            installation.Install(self);
        }

        #endregion

        #region Configure Fluent Registration

        /// <summary>
        /// 
        /// </summary>
        /// <param name="self"></param>
        /// <param name="optionsAction"></param>
        public static void Configure(this IServiceCollection self, Action<FluentRegistrationOptions> optionsAction)
        {
            if (self == null)
            {
                throw new ArgumentNullException(nameof(self));
            }
            if (optionsAction == null)
            {
                throw new ArgumentNullException(nameof(optionsAction));
            }

            var options = self.GetAttachedValue(ServiceCollectionAttachedProperties.Options) ?? new FluentRegistrationOptions();
            optionsAction(options);
            self.SetAttachedValue(ServiceCollectionAttachedProperties.Options, options);
        }

        #endregion

    }

}
