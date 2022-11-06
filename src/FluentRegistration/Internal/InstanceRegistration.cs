using FluentRegistration.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace FluentRegistration.Internal;

public class InstanceRegistration<T> :
    IWithServicesInitial,
    IRegister
    where T : class
{
    private readonly T _instance;
    private ServiceTypeSelector _serviceTypeSelector = new ServiceTypeSelector();

    public InstanceRegistration(T instance)
    {
        GuardAgainst.Null(instance);

        _instance = instance;
    }

    public IServiceSelector WithServices
    {
        get
        {
            return _serviceTypeSelector;
        }
    }

    public void Register(IServiceCollection services)
    {
        var serviceTypes = _serviceTypeSelector.GetServicesFor(_instance.GetType());
        var componentRegistration = new ComponentInstanceRegistration(serviceTypes, _instance);
        componentRegistration.Register(services);
    }
}
