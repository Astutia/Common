using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Astutia.Common.IoC.Adapter.Specific
{
    /// <summary>
    /// The IoC adapter for Unity.
    /// </summary>
    internal class UnityIoCAdapter : IoCReflectionAdapterBase
    {
        /// <summary>
        /// The type of the 'Unity.UnityContainerExtensions' class.
        /// </summary>
        private readonly TypeInfo unityContainerExtensionsTypeInfo = Type.GetType("Unity.UnityContainerExtensions, Unity.Abstractions", true).GetTypeInfo();

        /// <summary>
        /// The type of the 'Unity.Injection.InjectionFactory' class.
        /// </summary>
        private readonly Type injectionFactoryType = Type.GetType("Unity.Injection.InjectionFactory, Unity.Abstractions", true);

        /// <summary>
        /// The type of the 'Unity.Registration.InjectionMember' class.
        /// </summary>
        private readonly Type injectionMemberType = Type.GetType("Unity.Registration.InjectionMember, Unity.Abstractions", true);

        /// <summary>
        /// The types of parameters for register method.
        /// </summary>
        private readonly Type[] registerParameters;

        /// <summary>
        /// The types of parameters for register factory method.
        /// </summary>
        private readonly Type[] registerFactoryParameters;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnityIoCAdapter"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public UnityIoCAdapter(object container)
            : base(container)
        {
            Type unityContainerType = Type.GetType("Unity.IUnityContainer, Unity.Abstractions", true);
            Type injectionMemberArrayType = Array.CreateInstance(this.injectionMemberType, 0).GetType();
            this.registerParameters = new Type[]
            {
                unityContainerType,
                typeof(Type),
                typeof(Type),
                injectionMemberArrayType
            };
            this.registerFactoryParameters = new Type[] 
            {
                unityContainerType,
                typeof(Type),
                typeof(string),
                injectionMemberArrayType
            };
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
            // Invocation of IUnityContainer RegisterSingleton(this IUnityContainer container, Type from, Type to, params InjectionMember[] injectionMembers);
            // and IUnityContainer RegisterType(this IUnityContainer container, Type from, Type to, params InjectionMember[] injectionMembers);
            MethodInfo method = settings == IocRegisterSettings.Singleton
                                   ? this.GetMethod(this.unityContainerExtensionsTypeInfo, "RegisterSingleton", this.registerParameters, "RegisterSingletonForSimpleRegistration")
                                   : this.GetMethod(this.unityContainerExtensionsTypeInfo, "RegisterType", this.registerParameters, "RegisterTypeForSimpleRegistration");
            method.Invoke(null, new object[]
                   {
                       this.registrar,
                       typeof(TDependency),
                       typeof(TImplementation),
                       null
                   });
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
            // Invocation of IUnityContainer RegisterSingleton(this IUnityContainer container, Type t, string name, params InjectionMember[] injectionMembers);
            // and IUnityContainer RegisterType(this IUnityContainer container, Type t, string name, params InjectionMember[] injectionMembers);
            MethodInfo method = settings == IocRegisterSettings.Singleton
                                   ? this.GetMethod(this.unityContainerExtensionsTypeInfo, "RegisterSingleton", this.registerFactoryParameters, "RegisterSingletonForFactoryRegistration")
                                   : this.GetMethod(this.unityContainerExtensionsTypeInfo, "RegisterType", this.registerFactoryParameters, "RegisterTypeForFactoryRegistration");
            Array array = Array.CreateInstance(this.injectionMemberType, 1);
            array.SetValue(Activator.CreateInstance(this.injectionFactoryType, new object[] { new Func<object, object>(originalContainer => creationAction(this)) }), 0);
            method.Invoke(null, new object[]
                   {
                       this.registrar,
                       typeof(TObject),
                       null,
                       array
                   });
            return this;
        }

        /// <summary>
        /// Resolves an objects.
        /// </summary>
        /// <typeparam name="TObject">The type of the object to resolve.</typeparam>
        /// <returns>The resolved object.</returns>
        public override TObject Resolve<TObject>()
        {
            // Invocation of container.Resolve(typeof(TObject), null);
            object[] arguments = new object[] { typeof(TObject), null, null };
            return (TObject)GetMethod(this.Resolver, "Resolve", arguments.Length)
                            .Invoke(this.Resolver, arguments); 
        }
    }
}
