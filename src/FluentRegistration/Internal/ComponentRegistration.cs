﻿#region License
// Copyright (c) Niklas Wendel 2016
// 
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at 
// 
// http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License.
#endregion
using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using AttachedProperties;
using FluentRegistration.Options;

namespace FluentRegistration.Internal
{

    /// <summary>
    /// 
    /// </summary>
    public class ComponentRegistration<TService> : IRegistration, IFluentInterface
    {

        #region Fields

        private readonly Type[] _services;
        private Type _implementedBy;
        private readonly LifetimeDescriptor<ComponentRegistration<TService>> _lifetimeDescriptor;

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        internal ComponentRegistration()
        {
            _lifetimeDescriptor = new LifetimeDescriptor<ComponentRegistration<TService>>(this);
            _services = new[] { typeof(TService) };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        internal ComponentRegistration(params Type[] services)
        {
            _lifetimeDescriptor = new LifetimeDescriptor<ComponentRegistration<TService>>(this);
            _services = services;
        }

        #endregion

        #region Implemented By

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ComponentRegistration<TService> ImplementedBy<TImplementation>()
            where TImplementation : TService
        {
            return ImplementedBy(typeof(TImplementation));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ComponentRegistration<TService> ImplementedBy(Type type)
        {
            _implementedBy = type;
            return this;
        }

        #endregion

        #region Using Factory

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public FactoryRegistration UsingFactory()
        {
            var factoryRegistration = UsingFactory<IServiceFactory<TService>>();
            return factoryRegistration;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TFactory"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        /// <returns></returns>
        public FactoryRegistration UsingFactory<TFactory>()
            where TFactory : IServiceFactory<TService>
        {
            return UsingFactoryMethod(provider =>
            {
                var factory = provider.GetRequiredService<TFactory>();
                var implementation = factory.Create();
                return implementation;
            });
        }

        #endregion

        #region Using Factory Method

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TImplementation"></typeparam>
        /// <param name="factoryMethod"></param>
        /// <returns></returns>
        public FactoryRegistration UsingFactoryMethod(Func<TService> factoryMethod)
        {
            return UsingFactoryMethod(provider => factoryMethod());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TImplementation"></typeparam>
        /// <param name="factoryMethod"></param>
        /// <returns></returns>
        public FactoryRegistration UsingFactoryMethod(Func<IServiceProvider, TService> factoryMethod)
        {
            var factoryRegistration = new FactoryRegistration(serviceProvider => factoryMethod(serviceProvider), _services);
            return factoryRegistration;
        }

        #endregion

        #region Instance

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public InstanceRegistration Instance(object instance)
        {
            var instanceRegistration = new InstanceRegistration(instance, _services);
            return instanceRegistration;
        }

        #endregion

        #region Lifetime

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public LifetimeDescriptor<ComponentRegistration<TService>> Lifetime => _lifetimeDescriptor;

        #endregion

        #region Register

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceCollection"></param>
        void IRegistration.Register(IServiceCollection serviceCollection)
        {
            if(_implementedBy == null)
            {
                // No implementation type
                throw new RegistrationException("Cannot register component without ImplementedBy or Instance");
            }

            if(serviceCollection.Any(x => x.ImplementationType == _implementedBy))
            {
                // Already registered
                var options = serviceCollection.GetAttachedValue(ServiceCollectionAttachedProperties.Options) ?? FluentRegistrationOptions.Default;
                switch(options.MultipleRegistrationsBehavior)
                {
                    case MultipleRegistrationsBehavior.Ignore:
                        return;
                    case MultipleRegistrationsBehavior.ThrowException:
                        throw new RegistrationException(string.Format("Implementation of type {0} already registrered", _implementedBy.FullName));
                }
            }

            if(_services.Count() == 0)
            {
                // No interfaces found
                var options = serviceCollection.GetAttachedValue(ServiceCollectionAttachedProperties.Options) ?? FluentRegistrationOptions.Default;
                switch(options.RegistrationsWithoutServicesBehavior)
                {
                    case RegistrationsWithoutServicesBehavior.Ignore:
                        return;
                    case RegistrationsWithoutServicesBehavior.ThrowException:
                        throw new RegistrationException(string.Format("No services found for implementation of type {0}", _implementedBy.FullName));
                }
            }

            var lifetime = _lifetimeDescriptor.GetLifetime();

            var service = _services.First();
            var otherServices = _services.Skip(1).ToList();

            var serviceDescriptor = new ServiceDescriptor(service, _implementedBy, lifetime);
            serviceCollection.Add(serviceDescriptor);
            foreach (var otherService in otherServices)
            {
                var otherServiceDescriptor = new ServiceDescriptor(otherService, serviceProviders => serviceProviders.GetService(service), lifetime);
                serviceCollection.Add(otherServiceDescriptor);
            }
        }

        #endregion

    }

    /// <summary>
    /// 
    /// </summary>
    public class ComponentRegistration : ComponentRegistration<object>
    {

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        internal ComponentRegistration(params Type[] services) : base(services)
        {
        }

        #endregion

    }

}