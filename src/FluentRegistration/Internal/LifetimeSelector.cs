using Microsoft.Extensions.DependencyInjection;

namespace FluentRegistration.Internal
{
    public class LifetimeSelector :
        ILifetimeSelector,
        IValidRegistration
    {
        #region Properties

        public ServiceLifetime Lifetime { get; private set; } = ServiceLifetime.Singleton;

        #endregion

        #region Singleton

        public IValidRegistration Singleton()
        {
            Lifetime = ServiceLifetime.Singleton;
            return this;
        }

        #endregion

        #region Scoped

        public IValidRegistration Scoped()
        {
            Lifetime = ServiceLifetime.Scoped;
            return this;
        }

        #endregion

        #region Transient

        public IValidRegistration Transient()
        {
            Lifetime = ServiceLifetime.Transient;
            return this;
        }

        #endregion
    }
}
