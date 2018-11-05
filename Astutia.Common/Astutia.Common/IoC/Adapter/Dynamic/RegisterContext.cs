using System;
using System.Collections.Generic;
using System.Text;

namespace Astutia.Common.IoC.Adapter.Dynamic
{
    /// <summary>
    /// The register context.
    /// </summary>
    public struct RegisterContext
    {
        /// <summary>
        /// Gets or sets the dependency.
        /// </summary>
        public Type Dependency
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the implementation.
        /// </summary>
        public Type Implementation
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
