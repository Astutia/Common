using System;
using System.Collections.Generic;
using System.Text;

namespace Astutia.Common.IoC.Adapter.Dynamic
{
    /// <summary>
    /// The context for a dynamic IoC container.
    /// </summary>
    public interface IDynamicIoCContainerContext
    {
        /// <summary>
        /// Gets or sets a method which registers an object.
        /// </summary>
        Action<RegisterContext> Register
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a method which registers a factory for an object.
        /// </summary>
        Action<RegisterFactoryContext> RegisterFactory
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a method which resolves an object.
        /// </summary>
        Func<Type, object> Resolve
        {
            get;
            set;
        }
    }
}
