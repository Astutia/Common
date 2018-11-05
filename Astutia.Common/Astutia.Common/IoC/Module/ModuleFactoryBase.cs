using System;
using System.Collections.Generic;
using System.Text;
using Astutia.Common.IoC.Adapter;
using Astutia.Common.IoC.Container;

namespace Astutia.Common.IoC.Module
{
    /// <summary>
    /// The base class for a mechanism module factory.
    /// </summary>
    public abstract class ModuleFactoryBase
    {
        /// <summary>
        /// The synchronization object.
        /// </summary>
        private readonly object syncObject = new object();

        /// <summary>
        /// The IoC container.
        /// </summary>
        private IIoCContainer container;

        /// <summary>
        /// Gets the IoC container.
        /// </summary>
        protected IIoCContainer Container
        {
            get
            {
                if (this.container == null)
                {
                    lock (this.syncObject)
                    {
                        if (this.container == null)
                        {
                            this.container = new LightIoCContainer();
                            this.Register(this.container);
                        }
                    }
                }

                return this.container;
            }
        }

        /// <summary>
        /// Attaches mechnaism registrations to a specific IoC container.
        /// </summary>
        /// <param name="container">The container.</param>
        public void AttachToIoC(IIoCContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            lock (this.syncObject)
            {
                if (this.container != null)
                {
                    if (this.container is LightIoCContainer)
                    {
                        throw new InvalidOperationException("The mechanism was used before calling the Attach method. Make sure the Attach method is called before any mechanism usings.");
                    }
                    else
                    {
                        throw new InvalidOperationException($"The mechanism is already attached to an IoC container '{this.container}'.");
                    }
                }

                this.container = container;
                this.Register(this.container);
            }
        }

        /// <summary>
        /// Attaches mechnaism registrations to a specific IoC container.
        /// </summary>
        /// <param name="container">The container.</param>
        public void AttachToIoC(object container)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            this.AttachToIoC(new IoCContainerAdapterFactory().Create(container));
        }

        /// <summary>
        /// Registers all mechanism dependencies. 
        /// </summary>
        /// <param name="container">The IoC container.</param>
        protected abstract void Register(IIoCContainer container);
    }
}
