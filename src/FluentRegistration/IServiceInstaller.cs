using Microsoft.Extensions.DependencyInjection;

namespace FluentRegistration
{
    public interface IServiceInstaller
    {
        #region Install

        void Install(IServiceCollection services);

        #endregion
    }
}
