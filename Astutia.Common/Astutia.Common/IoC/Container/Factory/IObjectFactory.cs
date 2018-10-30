using System;
using System.Collections.Generic;
using System.Text;

namespace Astutia.Common.IoC.Container.Factory
{
    /// <summary>
    /// Represents an object factory.
    /// </summary>
    internal interface IObjectFactory
    {
        /// <summary>
        /// Creates an object.
        /// </summary>
        /// <param name="resolver">The resolver.</param>
        /// <returns>The created object.</returns>
        object Create(IIoCResolver resolver);
    }
}
