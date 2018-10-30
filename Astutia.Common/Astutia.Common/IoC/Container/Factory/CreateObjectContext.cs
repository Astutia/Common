using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;

namespace Astutia.Common.IoC.Container.Factory
{
    /// <summary>
    /// The context for an object creator method.
    /// </summary>
    internal class CreateObjectContext
    {
        /// <summary>
        /// The constructor.
        /// </summary>
        private Lazy<ConstructorInfo> constructor;

        /// <summary>
        /// The constructor arguments.
        /// </summary>
        private Lazy<Type[]> constructorArguments;

        /// <summary>
        /// initializes a new instance of the <see cref="CreateObjectContext"/> class.
        /// </summary>
        public CreateObjectContext()
        {
            this.constructor = new Lazy<ConstructorInfo>(() => 
            {
                IEnumerable<ConstructorInfo> constructors = this.Type.GetTypeInfo().DeclaredConstructors;
                int count = constructors.Count();

                if (count == 0)
                {
                    throw new InvalidOperationException($"No constructor for '{this.Type.FullName}'");
                }

                if (count > 1)
                {
                    throw new InvalidOperationException($"Too many constructors ('{count}') for '{this.Type.FullName}'. One constructor is expected.");
                }

                return constructors.First();
            });
            this.constructorArguments = new Lazy<Type[]>(() => this.Constructor.GetParameters().Select(parameter => parameter.ParameterType).ToArray());
        }

        /// <summary>
        /// Gets or sets the type to create.
        /// </summary>
        public Type Type
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the constructor.
        /// </summary>
        public ConstructorInfo Constructor
        {
            get
            {
                return this.constructor.Value;
            }
        }

        /// <summary>
        /// Gets the constructor arguments.
        /// </summary>
        public Type[] ConstructorArguments
        {
            get
            {
                return this.constructorArguments.Value;
            }
        }
    }
}
