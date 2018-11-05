using System;
using System.Collections.Generic;
using System.Text;

namespace Astutia.Common.IoC.Adapter.Dynamic
{
    /// <summary>
    /// The register factory context.
    /// </summary>
    public struct RegisterFactoryContext
    {
        /// <summary>
        /// Gets or sets the type of the object to register.
        /// </summary>
        public Type Type
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the object factory.
        /// </summary>
        public Func<object> ObjectFactory
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        public IocRegisterSettings Settings
        {
            get;
            set;
        }
    }
}
