using System;
using System.Collections.Generic;
using System.Text;

namespace Astutia.Common.IoC
{
    /// <summary>
    /// The abstraction of IoC container.
    /// </summary>
    public interface IIoCContainer : IIoCRegistrar,
                                     IIoCResolver
    {
    }
}
