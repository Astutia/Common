using System;
using System.Collections.Generic;
using System.Text;

namespace Astutia.Common.IoC.Adapter.Dynamic
{
    /// <summary>
    /// The dynamic IoC container.
    /// </summary>
    internal class DynamicIoCContainer : IIoCContainer
    {
        /// <summary>
        /// The context.
        /// </summary>
        private readonly DynamicIoCContainerContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicIoCContainer"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public DynamicIoCContainer(DynamicIoCContainerContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.context.Validate();
        }

        /// <summary>
        /// Registers an object.
        /// </summary>
        /// <typeparam name="TDependency">The type of the registration object in IoC.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="settings">The settings.</param>
        /// <returns>The registrar.</returns>
        public IIoCRegistrar Register<TDependency, TImplementation>(IocRegisterSettings settings) where TImplementation : TDependency
        {
            this.context.Register(new RegisterContext()
            {
                Dependency = typeof(TDependency),
                Implementation = typeof(TImplementation),
                Settings = settings
            });
            return this;
        }

        /// <summary>
        /// Registers a factory for an object.
        /// </summary>
        /// <typeparam name="TObject">The type of the registration object in IoC.</typeparam>
        /// <param name="creationAction">The factory method.</param>
        /// <param name="settings">The settings.</param>
        /// <returns>The registrar.</returns>
        public IIoCRegistrar Register<TObject>(Func<IIoCResolver, TObject> creationAction, IocRegisterSettings settings)
        {
            this.context.RegisterFactory(new RegisterFactoryContext()
            {
                Type = typeof(TObject),
                ObjectFactory = () => creationAction(this),
                Settings = settings
            });
            return this;
        }

        /// <summary>
        /// Resolves an objects.
        /// </summary>
        /// <param name="type">The type of the object to resolve.</param>
        /// <returns>The resolved object.</returns>
        public object Resolve(Type type)
        {
            return this.context.Resolve(type);
        }
    }
}
