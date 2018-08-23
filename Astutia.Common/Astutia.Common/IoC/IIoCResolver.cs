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
        /// <typeparam name="TObject">The type of the object to resolve.</typeparam>
        /// <returns>The resolved object.</returns>
        TObject Resolve<TObject>();
    }
}
