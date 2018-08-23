using System;
using System.Collections.Generic;
using System.Linq;
using Astutia.Common.IoC.Adapter.Specific;

namespace Astutia.Common.IoC.Adapter
{
    /// <summary>
    /// The factory for an IoC container adapter.
    /// </summary>
    internal class IoCContainerAdapterFactory
    {
        /// <summary>
        /// The dictionary with IoC adapters.
        /// </summary>
        private readonly Dictionary<string, Func<object, IIoCContainer>> adapters = new Dictionary<string, Func<object, IIoCContainer>>()
        {
            { "Ninject", container => new NinjectIoCAdapter(container) }
        };

        /// <summary>
        /// Creates an IoC adapter for a container.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <returns>The adapter.</returns>
        public IIoCContainer Create(object container)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            string name = container.GetType().Name;
            Func<object, IIoCContainer> creator = this.adapters.SingleOrDefault(adapter => adapter.Key.Contains(name)).Value;

            if (creator == null)
            {
                throw new NotSupportedException(string.Format(
                    "Could not create an adapter for IoC container '{0}', supported IoC containers:{1}{2}",
                    name,
                    Environment.NewLine,
                    string.Join(Environment.NewLine, this.adapters.Select(adapter => adapter.Key))));
            }

            return creator(container);
        }
    }
}
