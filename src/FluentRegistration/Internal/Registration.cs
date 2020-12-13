using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using FluentRegistration.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace FluentRegistration.Internal
{
    public class Registration : IRegistration
    {
        #region Fields

        private IRegister? _register;

        #endregion

        #region For

        public IComponentImplementationSelector<TService> For<TService>()
            where TService : class
        {
            var registration = new ComponentRegistration<TService>(new[] { typeof(TService) });
            _register = registration;
            return registration;
        }

        public IComponentImplementationSelector<object> For(Type type)
        {
            GuardAgainst.Null(type, nameof(type));

            return For(new[] { type });
        }

        public IComponentImplementationSelector<object> For(params Type[] types)
        {
            GuardAgainst.NullOrEmpty(types, nameof(types));

            var registration = new ComponentRegistration<object>(types);
            _register = registration;
            return registration;
        }

        #endregion

        #region From Assembly

        public ITypeSelector FromAssembly(Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            var registration = new AssemblyTypeSelector(assembly);
            _register = registration;
            return registration;
        }

        #endregion

        #region From Assembly Containing

        public ITypeSelector FromAssemblyContaining(Type type)
        {
            GuardAgainst.Null(type, nameof(type));

            var assembly = type.GetTypeInfo().Assembly;
            return FromAssembly(assembly);
        }

        public ITypeSelector FromAssemblyContaining<T>()
        {
            return FromAssemblyContaining(typeof(T));
        }

        #endregion

        #region From This Assembly

        [MethodImpl(MethodImplOptions.NoInlining)]
        public ITypeSelector FromThisAssembly()
        {
            return FromAssembly(Assembly.GetCallingAssembly());
        }

        #endregion

        #region Implemented By

        public IWithServicesInitial ImplementedBy<T>()
        {
            return FromAssemblyContaining<T>()
                .Where(c => c.ImplementationType == typeof(T));
        }

        #endregion

        #region Instance

        public IWithServicesInitial Instance<T>(T instance)
            where T : class
        {
            var registration = new InstanceRegistration<T>(instance);
            _register = registration;
            return registration;
        }

        #endregion

        #region Register

        public void Register(IServiceCollection services)
        {
            if (_register == null)
            {
                throw new InvalidOperationException("Register called without defining what to register via the fluent Api.");
            }

            _register.Register(services);
        }

        #endregion
    }
}
