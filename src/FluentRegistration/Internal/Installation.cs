using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;

namespace FluentRegistration.Internal
{
    public class Installation : IInstallation
    {
        #region Fields

        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1011:Closing square brackets should be spaced correctly", Justification = "Stylecop can't handle this")]
        private IServiceInstaller[]? _installers;

        #endregion

        #region From Assembly

        public void FromAssembly(Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            var allTypes = assembly.GetTypes();
            var installers = allTypes
                .Where(x => typeof(IServiceInstaller).GetTypeInfo().IsAssignableFrom(x))
                .Select(x => Activator.CreateInstance(x))
                .Cast<IServiceInstaller>()
                .ToArray();
            _installers = installers;
        }

        #endregion

        #region From Assembly Containing

        public void FromAssemblyContaining(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            var assembly = type.GetTypeInfo().Assembly;
            FromAssembly(assembly);
        }

        public void FromAssemblyContaining<T>()
        {
            FromAssemblyContaining(typeof(T));
        }

        #endregion

        #region From This Assembly

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void FromThisAssembly()
        {
            FromAssembly(Assembly.GetCallingAssembly());
        }

        #endregion

        #region Install

        public void Install(IServiceCollection services)
        {
            if (_installers == null)
            {
                throw new InvalidOperationException("Install called without defining what to install via the fluent Api.");
            }

            foreach (var installer in _installers)
            {
                installer.Install(services);
            }
        }

        #endregion
    }
}
