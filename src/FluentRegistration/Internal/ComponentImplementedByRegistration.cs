using AttachedProperties;
using FluentRegistration.Options;

namespace FluentRegistration.Internal;

public class ComponentImplementedByRegistration<TService, TImplementation> : ILifetime<IHasKeySelectorComponent>, IRegister
    where TImplementation : TService
{
    private readonly IEnumerable<Type> _serviceTypes;
    private readonly Type _implementedByType;
    private readonly LifetimeAndKeySelector<IHasKeySelectorComponent> _lifetimeAndKeySelector;

    public ComponentImplementedByRegistration(IEnumerable<Type> serviceTypes)
        : this(serviceTypes, typeof(TImplementation), new LifetimeAndKeySelector<IHasKeySelectorComponent>())
    {
    }

    public ComponentImplementedByRegistration(IEnumerable<Type> serviceTypes, Type implementedByType, LifetimeAndKeySelector<IHasKeySelectorComponent> lifetimeSelector)
    {
        _serviceTypes = serviceTypes;
        _implementedByType = implementedByType;
        _lifetimeAndKeySelector = lifetimeSelector;
    }

    public ILifetimeSelector<IHasKeySelectorComponent> Lifetime => _lifetimeAndKeySelector;

    public void Register(IServiceCollection services)
    {
        if (services.Any(x => x.ImplementationType == _implementedByType))
        {
            // Already registered
            var options = services.GetAttachedValue(ServiceCollectionAttachedProperties.Options) ?? FluentRegistrationOptions.Default;
            switch (options.MultipleRegistrationsBehavior)
            {
                case MultipleRegistrationsBehavior.Ignore:
                    return;
                case MultipleRegistrationsBehavior.Register:
                    break;
                case MultipleRegistrationsBehavior.ThrowException:
                    throw new RegistrationException($"Implementation of type {_implementedByType.FullName} already registered");
            }
        }

        if (_serviceTypes.None())
        {
            // No interfaces found
            var options = services.GetAttachedValue(ServiceCollectionAttachedProperties.Options) ?? FluentRegistrationOptions.Default;
            switch (options.RegistrationsWithoutServicesBehavior)
            {
                case RegistrationsWithoutServicesBehavior.Ignore:
                    return;
                case RegistrationsWithoutServicesBehavior.ThrowException:
                    throw new RegistrationException($"No services found for implementation of type {_implementedByType.FullName}");
            }
        }

        if (_serviceTypes.Count() == 1)
        {
            var serviceType = _serviceTypes.First();
            var serviceDescriptor = new ServiceDescriptor(serviceType, _lifetimeAndKeySelector.ComponentKey(serviceType, _implementedByType), _implementedByType, _lifetimeAndKeySelector.Lifetime);
            services.Add(serviceDescriptor);
        }
        else
        {
            // TODO: Workaround to solve problem with registering multiple implementation types under same shared interface.
            //       Since they should be resolved to same instance in case of singleton or scoped lifestyle.
            var selfServiceDescriptor = new ServiceDescriptor(_implementedByType, _lifetimeAndKeySelector.ComponentKey(_implementedByType, _implementedByType), _implementedByType, _lifetimeAndKeySelector.Lifetime);
            services.Add(selfServiceDescriptor);

            foreach (var serviceType in _serviceTypes)
            {
                if (serviceType == _implementedByType)
                {
                    // Already registered with self above
                    continue;
                }

                var serviceDescriptor = new ServiceDescriptor(serviceType, _lifetimeAndKeySelector.ComponentKey(serviceType, _implementedByType), (serviceProvider, serviceKey) => serviceProvider.GetRequiredService(_implementedByType), _lifetimeAndKeySelector.Lifetime);
                services.Add(serviceDescriptor);
            }
        }
    }
}
