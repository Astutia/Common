using System;
using System.Collections.Generic;
using System.Text;

namespace Astutia.Common.IoC
{
    /// <summary>
    /// Settings for the IoC registration.
    /// </summary>
    public enum IocRegisterSettings
    {
        /// <summary>
        /// No specific settings.
        /// </summary>
        None = 0,

        /// <summary>
        /// Indicates that the object should has only one instance.
        /// </summary>
        Singleton
    }
}
