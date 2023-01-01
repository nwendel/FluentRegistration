using AttachedProperties;
using FluentRegistration.Options;

namespace FluentRegistration;

public static class ServiceCollectionAttachedProperties
{
    public static readonly AttachedProperty<IServiceCollection, FluentRegistrationOptions> Options = new(nameof(Options));
}
