using System;
using System.Collections.Generic;
using System.Text;
using Astutia.Common.IoC.Adapter.Dynamic;

namespace Astutia.Common.IoC.Module
{
    /// <summary>
    /// Extensions for a module factory.
    /// </summary>
    public static class ModuleFactoryExtensions
    {
        /// <summary>
        /// Attaches mechnaism registrations to a dynamic IoC container.
        /// </summary>
        /// <typeparam name="TModule">The type of the module to attach.</typeparam>
        /// <param name="module">The module.</param>
        /// <param name="contextFactory">The dynaimc IoC context factory.</param>
        public static void AttachToIoC<TModule>(this TModule module, Action<IDynamicIoCContainerContext> contextFactory) where TModule : ModuleFactoryBase
        {
            DynamicIoCContainerContext context = new DynamicIoCContainerContext();
            contextFactory(context);
            module.AttachToIoC(new DynamicIoCContainer(context));
        }
    }
}
