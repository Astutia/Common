using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Astutia.Common.IoC;
using Astutia.Common.IoC.Adapter.Specific;
using Astutia.Common.Tests.IoC.TestObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace Astutia.Common.Tests.IoC.Adapter.Specific
{
    [TestClass]
    public class NinjectIoCAdapterTests : IoCContainerTestsBase
    {
        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
        }

        [TestMethod]
        [ExpectedException(typeof(TargetInvocationException))]
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
            return new NinjectIoCAdapter(kernel);
        }
    }
}
