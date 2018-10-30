using System;
using System.Collections.Generic;
using System.Text;

namespace Astutia.Common.IoC.Container
{
    /// <summary>
    /// The light IoC implementation.
    /// </summary>
    internal class LightIoCContainer : IIoCContainer
    {
        /// <summary>
        /// Registers an object.
        /// </summary>
        /// <typeparam name="TDependency">The type of the registration object in IoC.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="settings">The settings.</param>
        /// <returns>The registrar.</returns>
        public IIoCRegistrar Register<TDependency, TImplementation>(IocRegisterSettings settings) where TImplementation : TDependency
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Registers a factoryu for an object.
        /// </summary>
        /// <typeparam name="TObject">The type of the registration object in IoC.</typeparam>
        /// <param name="creationAction">The factory method.</param>
        /// <param name="settings">The settings.</param>
        /// <returns>The registrar.</returns>
        public IIoCRegistrar Register<TObject>(Func<IIoCResolver, TObject> creationAction, IocRegisterSettings settings)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Resolves an objects.
        /// </summary>
        /// <param name="type">The type of the object to resolve.</param>
        /// <returns>The resolved object.</returns>
        public object Resolve(Type type)
        {
            throw new NotImplementedException();
        }
    }
}
