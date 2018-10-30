using System;
using System.Collections.Generic;
using System.Text;

namespace Astutia.Common.IoC
{
    /// <summary>
    /// Extensions for IocRegisterSettings.
    /// </summary>
    internal static class IocRegisterSettingsExtensions
    {
        /// <summary>
        /// Checks if the settings contains the singleton setting.
        /// </summary>
        /// <param name="settings">The settings to check.</param>
        /// <returns><c>True</c> if the settings contains the singleton setting, otherwise <c>false</c>.</returns>
        public static bool IsSingleton(this IocRegisterSettings settings)
        {
            return (settings & IocRegisterSettings.Singleton) != 0;
        }
    }
}
