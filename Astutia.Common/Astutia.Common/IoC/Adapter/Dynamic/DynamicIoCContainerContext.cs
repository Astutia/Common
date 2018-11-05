using System;
using System.Collections.Generic;
using System.Text;

namespace Astutia.Common.IoC.Adapter.Dynamic
{
    /// <summary>
    /// The context for a dynamic IoC container.
    /// </summary>
    internal class DynamicIoCContainerContext : IDynamicIoCContainerContext
    {
        /// <summary>
        /// Gets or sets a method which registers an object.
        /// </summary>
        public Action<RegisterContext> Register
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a method which registers a factory for an object.
        /// </summary>
        public Action<RegisterFactoryContext> RegisterFactory
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a method which resolves an object.
        /// </summary>
        public Func<Type, object> Resolve
        {
            get;
            set;
        }

        /// <summary>
        /// Validates the context.
        /// </summary>
        public void Validate()
        {
            if (this.Register == null)
            {
                throw new NotSupportedException($"Property '{nameof(this.Register)}'  is not set.");
            }

            if (this.RegisterFactory == null)
            {
                throw new NotSupportedException($"Property '{nameof(this.RegisterFactory)}'  is not set.");
            }

            if (this.Resolve == null)
            {
                throw new NotSupportedException($"Property '{nameof(this.Resolve)}'  is not set.");
            }
        }
    }
}
