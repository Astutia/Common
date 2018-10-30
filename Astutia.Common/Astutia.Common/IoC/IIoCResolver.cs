using System;
using System.Collections.Generic;
using System.Text;

namespace Astutia.Common.IoC
{
    /// <summary>
    /// The abstraction for IoC resolver.
    /// </summary>
    public interface IIoCResolver
    {
        /// <summary>
        /// Resolves an objects.
        /// </summary>
        /// <param name="type">The type of the object to resolve.</param>
        /// <returns>The resolved object.</returns>
        object Resolve(Type type);
    }
}
