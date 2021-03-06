﻿using AttachedProperties;
using FluentRegistration.Options;
using Microsoft.Extensions.DependencyInjection;

namespace FluentRegistration
{
    public static class ServiceCollectionAttachedProperties
    {
        #region Attached Properties

        public static readonly AttachedProperty<IServiceCollection, FluentRegistrationOptions> Options = new AttachedProperty<IServiceCollection, FluentRegistrationOptions>(nameof(Options));

        #endregion
    }
}
