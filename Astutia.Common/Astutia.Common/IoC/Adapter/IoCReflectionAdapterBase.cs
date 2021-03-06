﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;

namespace Astutia.Common.IoC.Adapter
{
    /// <summary>
    /// The base class for an IoC container adapter.
    /// </summary>
    internal abstract class IoCReflectionAdapterBase : IIoCContainer
    {
        /// <summary>
        /// The registrar.
        /// </summary>
        protected readonly object registrar;

        /// <summary>
        /// The synchronization object.
        /// </summary>
        private readonly object syncObject = new object();

        /// <summary>
        /// The cache for reflection methods.
        /// </summary>
        private readonly Dictionary<string, MethodInfo> methodsCache = new Dictionary<string, MethodInfo>();

        /// <summary>
        /// Initializes a new instance of the <see cref="IoCReflectionAdapterBase"/> class.
        /// </summary>
        /// <param name="registrar">The registrar.</param>
        public IoCReflectionAdapterBase(object registrar)
        {
            this.registrar = registrar ?? throw new ArgumentNullException(nameof(registrar));
            this.Resolver = this.registrar;
        }

        /// <summary>
        /// Gets or sets the resolver.
        /// </summary>
        public object Resolver
        {
            get;
            set;
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
        /// Registers a factory for an object.
        /// </summary>
        /// <typeparam name="TObject">The type of the registration object in IoC.</typeparam>
        /// <param name="creationAction">The factory method.</param>
        /// <param name="settings">The settings.</param>
        /// <returns>The registrar.</returns>
        public abstract IIoCRegistrar Register<TObject>(Func<IIoCResolver, TObject> creationAction, IocRegisterSettings settings);

        /// <summary>
        /// Resolves an objects.
        /// </summary>
        /// <param name="type">The type of the object to resolve.</param>
        /// <returns>The resolved object.</returns>
        public abstract object Resolve(Type type);

        /// <summary>
        /// Gets a spoecific method.
        /// </summary>
        /// <param name="item">The object from which the method should be get.</param>
        /// <param name="name">The name of the method.</param>
        /// <param name="numberOfParameters">The number of parameters.</param>
        /// <returns>The method.</returns>
        protected MethodInfo GetMethod(object item, string name, int numberOfParameters)
        {
            return this.GetMethod(item.GetType().GetTypeInfo(), name, numberOfParameters);
        }

        /// <summary>
        /// Gets a spoecific method.
        /// </summary>
        /// <param name="info">The type info.</param>
        /// <param name="name">The name of the method.</param>
        /// <param name="numberOfParameters">The number of parameters.</param>
        /// <param name="cacheName">The cache name.</param>
        /// <returns>The method.</returns>
        protected MethodInfo GetMethod(TypeInfo info, string name, int numberOfParameters, string cacheName = null)
        {
            lock (this.syncObject)
            {
                MethodInfo result;

                if (this.methodsCache.TryGetValue(string.IsNullOrEmpty(cacheName) ? name : cacheName, out result))
                {
                    return result;
                }
            }

            MethodInfo method = GetMethods(info).FirstOrDefault(x => x.Name == name
                                                                && x.GetParameters().Length == numberOfParameters);

            if (method == null)
            {
                throw new InvalidOperationException($"No method '{name}'.");
            }

            lock (this.syncObject)
            {
                this.methodsCache[string.IsNullOrEmpty(cacheName) ? name : cacheName] = method;
            }

            return method;
        }

        /// <summary>
        /// Gets a spoecific method.
        /// </summary>
        /// <param name="info">The type info.</param>
        /// <param name="name">The name of the method.</param>
        /// <param name="parameters">The colleciton of parameters.</param>
        /// <param name="cacheName">The cache name.</param>
        /// <param name="cached">A value indicating whether the method is cached.</param>
        /// <returns>The method.</returns>
        protected MethodInfo GetMethod(TypeInfo info, string name, Type[] parameters, string cacheName = null, bool cached = true)
        {
            if (cached)
            {
                lock (this.syncObject)
                {
                    MethodInfo result;

                    if (this.methodsCache.TryGetValue(string.IsNullOrEmpty(cacheName) ? name : cacheName, out result))
                    {
                        return result;
                    }
                }
            }

            MethodInfo method = GetMethods(info).FirstOrDefault(x => x.Name == name
                                                                && x.GetParameters().Select(parameter => parameter.ParameterType).SequenceEqual(parameters));

            if (method == null)
            {
                throw new InvalidOperationException($"No method '{name}'.");
            }

            if (cached)
            {
                lock (this.syncObject)
                {
                    this.methodsCache[string.IsNullOrEmpty(cacheName) ? name : cacheName] = method;
                }
            }

            return method;
        }

        /// <summary>
        /// Gets all public methods for the type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>All public methods for the type.</returns>
        private static IEnumerable<MethodInfo> GetMethods(TypeInfo type)
        {
            TypeInfo current = type;

            while (true)
            {
                foreach (MethodInfo info in current.DeclaredMethods)
                {
                    yield return info;
                }

                if (current.BaseType == null)
                {
                    break;
                }

                current = current.BaseType.GetTypeInfo();
            }
        }
    }
}
