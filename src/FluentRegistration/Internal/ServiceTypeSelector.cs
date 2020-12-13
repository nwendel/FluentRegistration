using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FluentRegistration.Internal
{
    public class ServiceTypeSelector :
        IWithServices
    {
        #region Fields

        private readonly List<Func<Type, IEnumerable<Type>>> _serviceTypeSelectors = new List<Func<Type, IEnumerable<Type>>>();
        private readonly LifetimeSelector _lifetimeSelector = new LifetimeSelector();

        #endregion

        #region All Interfaces

        public IWithServices AllInterfaces()
        {
            _serviceTypeSelectors.Add(type => type.GetTypeInfo().GetInterfaces());
            return this;
        }

        #endregion

        #region Default Interface

        public IWithServices DefaultInterface()
        {
            _serviceTypeSelectors.Add(type =>
            {
                var typeInfo = type.GetTypeInfo();
                var interfaces = typeInfo.GetInterfaces();
                var defaultInterfaces = interfaces.Where(i =>
                {
                    var name = i.Name;
                    if (name.Length > 1 && name[0] == 'I' && char.IsUpper(name[1]))
                    {
                        name = name.Substring(1);
                    }

                    return type.Name.Contains(name);
                });
                return defaultInterfaces;
            });

            return this;
        }

        #endregion

        #region Service

        public IWithServices Service<TService>()
        {
            _serviceTypeSelectors.Add(type => new[] { typeof(TService) });
            return this;
        }

        #endregion

        #region Self

        public IWithServices Self()
        {
            _serviceTypeSelectors.Add(type => new[] { type });
            return this;
        }

        #endregion

        #region Get Services For

        public IEnumerable<Type> GetServicesFor(Type type)
        {
            return _serviceTypeSelectors.SelectMany(selector => selector(type));
        }

        #endregion

        #region Lifetime

        public ILifetimeSelector Lifetime => _lifetimeSelector;

        #endregion

        #region Get Lifetime Selector

        public LifetimeSelector GetLifetimeSelector() => _lifetimeSelector;

        #endregion
    }
}
