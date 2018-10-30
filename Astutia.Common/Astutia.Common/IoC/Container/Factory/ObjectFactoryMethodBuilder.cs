using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Astutia.Common.IoC.Container.Factory
{
    /// <summary>
    /// The context for an object factory.
    /// </summary>
    internal class ObjectFactoryMethodBuilder
    {
        /// <summary>
        /// The factory method.
        /// </summary>
        private Func<IIoCResolver, object> factory;

        /// <summary>
        /// Sets an implementation type.
        /// </summary>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <returns>The builder.</returns>
        public ObjectFactoryMethodBuilder ImplementationType<TImplementation>()
        {
            this.AssertFactoryMethodNotBuilt();
            CreateObjectContext context = new CreateObjectContext() { Type = typeof(TImplementation) };
            this.factory = resolver => CreateObject(resolver, context);
            return this;
        }

        /// <summary>
        /// Sets the facotry method.
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <param name="factoryMethod"></param>
        /// <returns>The builder.</returns>
        public ObjectFactoryMethodBuilder FactoryMethod<TObject>(Func<IIoCResolver, TObject> factoryMethod)
        {
            this.AssertFactoryMethodNotBuilt();
            this.factory = resolver => factoryMethod(resolver);
            return this;
        }

        /// <summary>
        /// Builds the factory method.
        /// </summary>
        /// <returns>The facotry method.</returns>
        public Func<IIoCResolver, object> Build()
        {
            return this.factory ?? throw new InvalidOperationException("Factory method not built");
        }

        /// <summary>
        /// Asserts that the factory method is not built yet.
        /// </summary>
        private void AssertFactoryMethodNotBuilt()
        {
            if (this.factory != null)
            {
                throw new InvalidOperationException("The factory method has already been built.");
            }
        }

        /// <summary>
        /// Creates an object.
        /// </summary>
        /// <param name="resolver">The resolver.</param>
        /// <param name="context">The context.</param>
        /// <returns>The created object.</returns>
        private static object CreateObject(IIoCResolver resolver, CreateObjectContext context)
        {
            object[] parameters = context.ConstructorArguments
                                         .Select(type => resolver.Resolve(type))
                                         .ToArray();
            return context.Constructor.Invoke(parameters);
        }
    }
}
