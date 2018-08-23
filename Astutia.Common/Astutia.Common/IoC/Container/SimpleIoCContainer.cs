using System;
using System.Collections.Generic;
using System.Text;

namespace Astutia.Common.IoC.Container
{
    /// <summary>
    /// The basic IoC implementation.
    /// </summary>
    internal class SimpleIoCContainer : IIoCContainer
    {
        public IIoCRegistrar Register<TDependency, TImplementation>(IocRegisterSettings settings) where TImplementation : TDependency
        {
            throw new NotImplementedException();
        }

        public IIoCRegistrar Register<TObject>(Func<IIoCResolver, TObject> creationAction, IocRegisterSettings settings)
        {
            throw new NotImplementedException();
        }

        public TObject Resolve<TObject>()
        {
            throw new NotImplementedException();
        }
    }
}
