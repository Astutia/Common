using Astutia.Common.IoC.Container.Factory;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Astutia.Common.IoC.Container
{
    /// <summary>
    /// The light IoC implementation.
    /// </summary>
    internal class LightIoCContainer : IIoCContainer
    {
        /// <summary>
        /// The colelciton of factories.
        /// </summary>
        private readonly ConcurrentDictionary<Type, IObjectFactory> factories = new ConcurrentDictionary<Type, IObjectFactory>();

        /// <summary>
        /// Registers an object.
        /// </summary>
        /// <typeparam name="TDependency">The type of the registration object in IoC.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="settings">The settings.</param>
        /// <returns>The registrar.</returns>
        public IIoCRegistrar Register<TDependency, TImplementation>(IocRegisterSettings settings) where TImplementation : TDependency
        {
            return this.InternalRegister<TDependency>(new ObjectFactoryMethodBuilder().ImplementationType<TImplementation>(), settings);
        }

        /// <summary>
        /// Registers a factory for an object.
        /// </summary>
        /// <typeparam name="TObject">The type of the registration object in IoC.</typeparam>
        /// <param name="creationAction">The factory method.</param>
        /// <param name="settings">The settings.</param>
        /// <returns>The registrar.</returns>
        public IIoCRegistrar Register<TObject>(Func<IIoCResolver, TObject> creationAction, IocRegisterSettings settings)
        {
            return this.InternalRegister<TObject>(new ObjectFactoryMethodBuilder().FactoryMethod<TObject>(creationAction), settings);
        }

        /// <summary>
        /// Resolves an objects.
        /// </summary>
        /// <param name="type">The type of the object to resolve.</param>
        /// <returns>The resolved object.</returns>
        public object Resolve(Type type)
        {
            IObjectFactory factory;

            if (!this.factories.TryGetValue(type, out factory))
            {
                throw new InvalidOperationException($"No IoC registration for type '{type}'");
            }

            return factory.Create(this);
        }

        /// <summary>
        /// Registers a factory for an object.
        /// </summary>
        /// <typeparam name="TObject">The type of the registration object in IoC.</typeparam>
        /// <param name="builder">The factory method builder.</param>
        /// <param name="settings">The settings.</param>
        /// <returns>The registrar.</returns>
        private IIoCRegistrar InternalRegister<TObject>(ObjectFactoryMethodBuilder builder, IocRegisterSettings settings)
        {
            Func<IIoCResolver, object> factoryMethod = builder.Build();
            this.factories[typeof(TObject)] = settings.IsSingleton()
                                               ? new SingleObjectFactory(factoryMethod)
                                               : new ObjectFactory(factoryMethod);
            return this;
        }
    }
}
