using System;
using AttachedProperties;
using FluentRegistration.Internal;
using FluentRegistration.Options;
using Microsoft.Extensions.DependencyInjection;

namespace FluentRegistration
{
    public static class ServiceCollectionExtensions
    {
        #region Register

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

        public static void Install<TInstaller>(this IServiceCollection self)
            where TInstaller : IServiceInstaller, new()
        {
            var installer = new TInstaller();
            installer.Install(self);
        }

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

        #region Configure

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
