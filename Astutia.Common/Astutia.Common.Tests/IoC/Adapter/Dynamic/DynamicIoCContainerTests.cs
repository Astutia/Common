using System;
using System.Collections.Generic;
using System.Text;
using Astutia.Common.IoC;
using Astutia.Common.IoC.Adapter.Dynamic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Ninject.Syntax;

namespace Astutia.Common.Tests.IoC.Adapter.Dynamic
{
    [TestClass]
    public class DynamicIoCContainerTests : IoCContainerTestsBase
    {
        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
        }

        [TestMethod]
        [ExpectedException(typeof(Ninject.ActivationException))]
        public override void WhenNoRegistrationAndResolve_ShouldThrowException()
        {
            base.WhenNoRegistrationAndResolve_ShouldThrowException();
        }

        [TestMethod]
        public override void WhenRegister_ShouldGetLevel1Properly()
        {
            base.WhenRegister_ShouldGetLevel1Properly();
        }

        [TestMethod]
        public override void WhenRegister_ShouldGetLevel2Properly()
        {
            base.WhenRegister_ShouldGetLevel2Properly();
        }

        [TestMethod]
        public override void WhenRegister_ShouldGetLevel3Properly()
        {
            base.WhenRegister_ShouldGetLevel3Properly();
        }

        [TestMethod]
        public override void WhenRegisterFactory_ShouldGetLevel1Properly()
        {
            base.WhenRegisterFactory_ShouldGetLevel1Properly();
        }

        [TestMethod]
        public override void WhenRegisterFactory_ShouldGetLevel2Properly()
        {
            base.WhenRegisterFactory_ShouldGetLevel2Properly();
        }

        [TestMethod]
        public override void WhenRegisterFactory_ShouldGetLevel3Properly()
        {
            base.WhenRegisterFactory_ShouldGetLevel3Properly();
        }

        [TestMethod]
        public override void WhenRegisterAsSingleton_ShouldCreateOnce()
        {
            base.WhenRegisterAsSingleton_ShouldCreateOnce();
        }

        [TestMethod]
        public override void WhenRegisterAsSingleton_ShouldCreateOnce2()
        {
            base.WhenRegisterAsSingleton_ShouldCreateOnce2();
        }

        [TestMethod]
        public override void WhenRegisterFactorySingleton_ShouldGetLevel1Properly()
        {
            base.WhenRegisterFactorySingleton_ShouldGetLevel1Properly();
        }

        [TestMethod]
        public override void WhenRegisterFactorySingleton_ShouldGetLevel2Properly()
        {
            base.WhenRegisterFactorySingleton_ShouldGetLevel2Properly();
        }

        [TestMethod]
        public override void WhenRegisterFactorySingleton_ShouldGetLevel3Properly()
        {
            base.WhenRegisterFactorySingleton_ShouldGetLevel3Properly();
        }

        protected override IIoCContainer CreateTarget()
        {
            IKernel kernel = new StandardKernel();
            DynamicIoCContainerContext context = new DynamicIoCContainerContext()
            {
                Register = registerContext =>
                {
                    IBindingWhenInNamedWithOrOnSyntax<object> binding = kernel.Bind(registerContext.Dependency)
                                                                              .To(registerContext.Implementation);

                    if (registerContext.Settings.IsSingleton())
                    {
                        binding.InSingletonScope();
                    }
                },
                RegisterFactory = registerFactoryContext => 
                {
                    IBindingWhenInNamedWithOrOnSyntax<object> binding = kernel.Bind(registerFactoryContext.Type)
                                                                              .ToMethod(internalContext => registerFactoryContext.ObjectFactory());

                    if (registerFactoryContext.Settings.IsSingleton())
                    {
                        binding.InSingletonScope();
                    }
                },
                Resolve = typeToResolve => kernel.GetService(typeToResolve)
            };
            return new DynamicIoCContainer(context);
        }
    }
}
