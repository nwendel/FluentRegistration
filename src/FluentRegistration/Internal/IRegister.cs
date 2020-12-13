using Microsoft.Extensions.DependencyInjection;

namespace FluentRegistration.Internal
{
    public interface IRegister
    {
        #region Register

        void Register(IServiceCollection services);

        #endregion
    }
}
