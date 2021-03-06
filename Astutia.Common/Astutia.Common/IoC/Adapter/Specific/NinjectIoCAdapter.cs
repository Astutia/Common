﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;

namespace Astutia.Common.IoC.Adapter.Specific
{
    /// <summary>
    /// The IoC adapter for Ninject.
    /// </summary>
    internal class NinjectIoCAdapter : IoCReflectionAdapterBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NinjectIoCAdapter"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public NinjectIoCAdapter(object container)
            : base(container)
        {
        }

        /// <summary>
        /// Registers an object.
        /// </summary>
        /// <typeparam name="TDependency">The type of the registration object in IoC.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="settings">The settings.</param>
        /// <returns>The registrar.</returns>
        public override IIoCRegistrar Register<TDependency, TImplementation>(IocRegisterSettings settings)
        {
            // Invocation of kernel.Bind(typeof(TDependency)).To(typeof(TImplementation));
            object[] bindArguments = new object[] { new Type[] { typeof(TDependency) } };
            object bindResult = this.GetMethod(this.registrar, "Bind", bindArguments.Length).Invoke(this.registrar, bindArguments);
            object[] toArguments = new object[] { typeof(TImplementation) };
            object toResult = this.GetMethod(bindResult, "To", toArguments.Length).Invoke(bindResult, toArguments);

            if (settings.IsSingleton())
            {
                this.GetMethod(toResult, "InSingletonScope", 0).Invoke(toResult, new object[0]);
            }

            return this;
        }

        /// <summary>
        /// Registers a factory for an object.
        /// </summary>
        /// <typeparam name="TObject">The type of the registration object in IoC.</typeparam>
        /// <param name="creationAction">The factory method.</param>
        /// <param name="settings">The settings.</param>
        /// <returns>The registrar.</returns>
        public override IIoCRegistrar Register<TObject>(Func<IIoCResolver, TObject> creationAction, IocRegisterSettings settings)
        {
            // Invocation of kernel.Bind(typeof(TDependency)).ToMethod(context => creationAction(this));
            object[] bindArguments = new object[] { new Type[] { typeof(TObject) } };
            object bindResult = this.GetMethod(this.registrar, "Bind", bindArguments.Length).Invoke(this.registrar, bindArguments);
            object[] toMethodArguments = new object[] { new Func<object, TObject>((object originalContainer) => creationAction(this)) };
            object toMethodResult = this.GetMethod(bindResult, "ToMethod", toMethodArguments.Length).Invoke(bindResult, toMethodArguments);

            if (settings.IsSingleton())
            {
                this.GetMethod(toMethodResult, "InSingletonScope", 0).Invoke(toMethodResult, new object[0]);
            }

            return this;
        }

        /// <summary>
        /// Resolves an objects.
        /// </summary>
        /// <param name="type">The type of the object to resolve.</param>
        /// <returns>The resolved object.</returns>
        public override object Resolve(Type type)
        {
            // Invocation of kernel.GetService(typeof(TObject));
            object[] arguments = new object[] { type };
            return this.GetMethod(this.Resolver, "System.IServiceProvider.GetService", arguments.Length)
                       .Invoke(this.Resolver, arguments);
        }
    }
}
