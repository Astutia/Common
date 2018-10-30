using System;
using System.Collections.Generic;
using System.Text;

namespace Astutia.Common.IoC
{
    /// <summary>
    /// Extensions for IoC resolver.
    /// </summary>
    public static class IoCResolverExtensions
    {
        /// <summary>
        /// Resolves an objects.
        /// </summary>
        /// <typeparam name="TObject">The type of the object to resolve.</typeparam>
        /// <returns>The resolved object.</returns>
        public static TObject Resolve<TObject>(this IIoCResolver resolver)
        {
            return (TObject)resolver.Resolve(typeof(TObject));
        }
    }
}
