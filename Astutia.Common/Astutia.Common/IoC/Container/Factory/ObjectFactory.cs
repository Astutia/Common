using System;
using System.Collections.Generic;
using System.Text;

namespace Astutia.Common.IoC.Container.Factory
{
    /// <summary>
    /// The object factory.
    /// </summary>
    internal class ObjectFactory : IObjectFactory
    {
        /// <summary>
        /// The factory.
        /// </summary>
        protected readonly Func<IIoCResolver, object> factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectFactory"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public ObjectFactory(Func<IIoCResolver, object> factory)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        /// <summary>
        /// Creates an object.
        /// </summary>
        /// <param name="resolver">The resolver.</param>
        /// <returns>The created object.</returns>
        public virtual object Create(IIoCResolver resolver)
        {
            return this.factory(resolver);
        }
    }
}
