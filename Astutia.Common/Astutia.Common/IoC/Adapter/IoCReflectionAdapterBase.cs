using System;
using System.Collections.Generic;
using System.Text;

namespace Astutia.Common.IoC.Adapter
{
    /// <summary>
    /// The base class for an IoC container adapter.
    /// </summary>
    internal abstract class IoCReflectionAdapterBase : IIoCContainer
    {
        /// <summary>
        /// The container.
        /// </summary>
        protected readonly object container;

        /// <summary>
        /// Initializes a new instance of the <see cref="IoCReflectionAdapterBase"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public IoCReflectionAdapterBase(object container)
        {
            this.container = container ?? throw new ArgumentNullException(nameof(container));
        }

        /// <summary>
        /// Registers an object.
        /// </summary>
        /// <typeparam name="TDependency">The type of the registration object in IoC.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="settings">The settings.</param>
        /// <returns>The registrar.</returns>
        public abstract IIoCRegistrar Register<TDependency, TImplementation>(IocRegisterSettings settings) where TImplementation : TDependency;

        /// <summary>
        /// Registers a factoryu for an object.
        /// </summary>
        /// <typeparam name="TObject">The type of the registration object in IoC.</typeparam>
        /// <param name="creationAction">The factory method.</param>
        /// <param name="settings">The settings.</param>
        /// <returns>The registrar.</returns>
        public abstract IIoCRegistrar Register<TObject>(Func<IIoCResolver, TObject> creationAction, IocRegisterSettings settings);

        /// <summary>
        /// Resolves an objects.
        /// </summary>
        /// <typeparam name="TObject">The type of the object to resolve.</typeparam>
        /// <returns>The resolved object.</returns>
        public abstract TObject Resolve<TObject>();
    }
}
