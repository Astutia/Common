using Astutia.Common.IoC.Adapter;
using System;
using System.Collections.Generic;
using System.Text;

namespace Astutia.Common.IoC
{
    /// <summary>
    /// Extensions for IoC adapter.
    /// </summary>
    public static class IoCAdapterExtensions
    {
        /// <summary>
        /// Sets a resolve container used for resolving objects (only for adapters which cannot use the same object for registration and resolving).
        /// </summary>
        /// <param name="resolver">The resolver.</param>
        /// <param name="resolverContainer">The resolver container.</param>
        public static void SetResolveContainer(this IIoCResolver resolver, object resolverContainer)
        {
            IoCReflectionAdapterBase adapterBase = resolver as IoCReflectionAdapterBase;

            if (adapterBase == null)
            {
                throw new InvalidOperationException($"Cannot set a resolver to {resolver.GetType().FullName} as the resolver is not an adapter.");
            }

            adapterBase.Resolver = resolverContainer;
        }
    }
}
