using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Astutia.Common.IoC.Adapter.Specific
{
    /// <summary>
    /// The IoC adapter for Autofac.
    /// </summary>
    internal class AutofacIoCAdapter : IoCReflectionAdapterBase
    {
        /// <summary>
        /// The type of the 'Unity.UnityContainerExtensions' class.
        /// </summary>
        private readonly TypeInfo autofacContainerExtensionsTypeInfo = Type.GetType("Autofac.RegistrationExtensions, Autofac", true).GetTypeInfo();

        /// <summary>
        /// The type of the 'Unity.UnityContainerExtensions' class.
        /// </summary>
        private readonly TypeInfo autofacResolutionExtensions = Type.GetType("Autofac.ResolutionExtensions, Autofac", true).GetTypeInfo();

        /// <summary>
        /// The type of the 'Autofac.IComponentContext' class.
        /// </summary>
        private readonly Type componentContextType = Type.GetType("Autofac.IComponentContext, Autofac", true);

        /// <summary>
        /// The type of the 'Autofac.ContainerBuilder' class.
        /// </summary>
        private readonly Type autofacyContainerType = Type.GetType("Autofac.ContainerBuilder, Autofac", true);

        /// <summary>
        /// The types of parameters for register method.
        /// </summary>
        private readonly Type[] registerParameters;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutofacIoCAdapter"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public AutofacIoCAdapter(object container)
            : base(container)
        {
            this.registerParameters = new Type[]
            {
                this.autofacyContainerType,
                typeof(Type)
            };
            this.Resolver = null;
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
            this.AssertNoResolver();
            // Invocation of containerBuilder.RegisterType(typeof(TDependency)).As(typeof(TImplementation));
            MethodInfo registerMethod = this.GetMethod(this.autofacContainerExtensionsTypeInfo, "RegisterType", this.registerParameters);
            object registrationResult = registerMethod.Invoke(null, new object[] 
            {
                this.registrar,
                typeof(TImplementation)
            });
            this.ContinueRegistration<TDependency>(registrationResult, settings);
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
            this.AssertNoResolver();
            // Invocation of containerBuilder.Register<TObject>(Func<..., TObject>).As(typeof(TObject))
            MethodInfo registerMethod = this.GetMethod(this.autofacContainerExtensionsTypeInfo, "Register", 2);
            registerMethod = registerMethod.MakeGenericMethod(typeof(TObject));
            object registrationResult = registerMethod.Invoke(null, new object[]
            {
                this.registrar,
                new Func<object, TObject>(originalContainer => creationAction(this))
            });
            this.ContinueRegistration<TObject>(registrationResult, settings);
            return this;
        }

        /// <summary>
        /// Resolves an objects.
        /// </summary>
        /// <typeparam name="TObject">The type of the object to resolve.</typeparam>
        /// <returns>The resolved object.</returns>
        public override TObject Resolve<TObject>()
        {
            if (this.Resolver == null)
            {
                throw new InvalidOperationException($"No resolver, you need to invoke {typeof(IoCAdapterExtensions).FullName + "." + nameof(IoCAdapterExtensions.SetResolveContainer)} method on this container with an instance of {this.componentContextType.FullName} as a parameter.");
            }

            // Invocation of componentContext.Resolve(typeof(TObject));
            MethodInfo resolveMethod = this.GetMethod(this.autofacResolutionExtensions, "Resolve", new Type[] { this.componentContextType, typeof(Type) });
            object resolveResult = resolveMethod.Invoke(null, new object[] { this.Resolver, typeof(TObject) });
            return (TObject)resolveResult;
        }

        /// <summary>
        /// Continues the process of registration.
        /// </summary>
        /// <typeparam name="TObject">The type of the registration object in IoC.</typeparam>
        /// <param name="registrationResult">The registration result.</param>
        /// <param name="settings">The settings.</param>
        private void ContinueRegistration<TObject>(object registrationResult, IocRegisterSettings settings)
        {
            MethodInfo asMethod = this.GetMethod(registrationResult.GetType().GetTypeInfo(), "As", new Type[] { typeof(Type[]) }, cached: false);
            object asResult = asMethod.Invoke(registrationResult, new object[] { new Type[] { typeof(TObject) } });

            if (settings == IocRegisterSettings.Singleton)
            {
                MethodInfo singletonInstanceMethod = this.GetMethod(asResult.GetType().GetTypeInfo(), "SingleInstance", new Type[0]);
                singletonInstanceMethod.Invoke(asResult, new object[0]);
            }
        }

        /// <summary>
        /// Asserts that there is no resolver.
        /// </summary>
        private void AssertNoResolver()
        {
            if (this.Resolver != null)
            {
                throw new InvalidOperationException("You cannot register after the autofac container has been built.");
            }
        }
    }
}
