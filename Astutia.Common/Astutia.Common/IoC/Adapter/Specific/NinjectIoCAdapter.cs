using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;

namespace Astutia.Common.IoC.Adapter.Specific
{
    /// <summary>
    /// The IoC adapter for Ninject.
    /// </summary>
    internal class NinjectIoCAdapter : IoCReflectionAdapterBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NinjectIoCAdapter"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public NinjectIoCAdapter(object container)
            : base(container)
        {
        }

        /// <summary>
        /// Registers an object.
        /// </summary>
        /// <typeparam name="TDependency">The type of the registration object in IoC.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="settings">The settings.</param>
        /// <returns>The registrar.</returns>
        public override IIoCRegistrar Register<TDependency, TImplementation>(IocRegisterSettings settings)
        {
            this.InvokePublicMethod(this.InvokePublicMethod(this.container, "Bind", new object[] { new Type[] { typeof(TDependency) } }), "To", new object[] { typeof(TImplementation) });
            return this;
        }

        /// <summary>
        /// Registers a factoryu for an object.
        /// </summary>
        /// <typeparam name="TObject">The type of the registration object in IoC.</typeparam>
        /// <param name="creationAction">The factory method.</param>
        /// <param name="settings">The settings.</param>
        /// <returns>The registrar.</returns>
        public override IIoCRegistrar Register<TObject>(Func<IIoCResolver, TObject> creationAction, IocRegisterSettings settings)
        {
            this.InvokePublicMethod(this.InvokePublicMethod(this.container, "Bind", new object[] { new Type[] { typeof(TObject) } }), "ToMethod", new object[] { new Func<object, TObject>((object a) => creationAction(this)) });
            return this;
        }

        /// <summary>
        /// Resolves an objects.
        /// </summary>
        /// <typeparam name="TObject">The type of the object to resolve.</typeparam>
        /// <returns>The resolved object.</returns>
        public override TObject Resolve<TObject>()
        {
            return (TObject)this.InvokePublicMethod(this.container, "System.IServiceProvider.GetService", new object[] { typeof(TObject) });
        }

        // TODO
        protected object InvokePublicMethod(object item, string name, object[] parameters)
        {
            TypeInfo info = item.GetType().GetTypeInfo();
            MethodInfo method = GetMethods(info).FirstOrDefault(x => x.Name == name
                                                                     && x.GetParameters().Length == parameters.Length);
            return method.Invoke(item, parameters);
        }

        public static IEnumerable<MethodInfo> GetMethods(TypeInfo type)
        {
            TypeInfo current = type;

            while (true)
            {
                foreach (MethodInfo info in current.DeclaredMethods)
                {
                    yield return info;
                }

                if (current.BaseType == null)
                {
                    break;
                }

                current = current.BaseType.GetTypeInfo();
            }
        }
    }
}
