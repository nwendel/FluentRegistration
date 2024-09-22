namespace FluentRegistration.Internal;

public class ComponentInstanceRegistration : IValidRegistration, IRegister
{
    private readonly IEnumerable<Type> _serviceTypes;
    private readonly object _instance;

    public ComponentInstanceRegistration(IEnumerable<Type> serviceTypes, object instance)
    {
        _serviceTypes = serviceTypes;
        _instance = instance;
    }

    public void Register(IServiceCollection services)
    {
        GuardAgainst.Null(services);

        foreach (var serviceType in _serviceTypes)
        {
            var serviceDescriptor = new ServiceDescriptor(serviceType, _instance);
            services.Add(serviceDescriptor);
        }
    }
}
