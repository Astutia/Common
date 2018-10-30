using System;
using System.Collections.Generic;
using System.Text;

namespace Astutia.Common.IoC.Container.Factory
{
    /// <summary>
    /// The object factory for singleton object.
    /// </summary>
    internal class SingleObjectFactory : ObjectFactory
    {
        /// <summary>
        /// The syunchronization object.
        /// </summary>
        private readonly object syncObject = new object();

        /// <summary>
        /// The object instance.
        /// </summary>
        private object instance;

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleObjectFactory"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public SingleObjectFactory(Func<IIoCResolver, object> factory)
            : base(factory)
        {
        }

        /// <summary>
        /// Creates an object.
        /// </summary>
        /// <param name="resolver">The resolver.</param>
        /// <returns>The created object.</returns>
        public override object Create(IIoCResolver resolver)
        {
            if (this.instance == null)
            {
                lock (this.syncObject)
                {
                    if (this.instance == null)
                    {
                        this.instance = this.factory(resolver);
                    }
                }
            }

            return this.instance;
        }
    }
}
