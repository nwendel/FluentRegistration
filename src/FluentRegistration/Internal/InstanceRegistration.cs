using System;
using Microsoft.Extensions.DependencyInjection;

namespace FluentRegistration.Internal
{
    public class InstanceRegistration<T> :
        IWithServicesInitial,
        IRegister
        where T : notnull
    {
        #region Fields

        private readonly T _instance;
        private ServiceTypeSelector _serviceTypeSelector = new ServiceTypeSelector();

        #endregion

        #region Constructor

        public InstanceRegistration(T instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            _instance = instance;
        }

        #endregion

        #region With Service

        public IServiceSelector WithServices
        {
            get
            {
                return _serviceTypeSelector;
            }
        }

        #endregion

        #region Register

        public void Register(IServiceCollection services)
        {
            var serviceTypes = _serviceTypeSelector.GetServicesFor(_instance.GetType());
            var componentRegistration = new ComponentInstanceRegistration(serviceTypes, _instance);
            componentRegistration.Register(services);
        }

        #endregion
    }
}
