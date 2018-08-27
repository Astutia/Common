using Astutia.Common.IoC;
using Astutia.Common.IoC.Adapter.Specific;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Unity;

namespace Astutia.Common.Tests.IoC.Adapter.Specific
{
    [TestClass]
    public class UnityIoCAdapterTests : IoCAdapterTestsBase
    {
        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
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
            IUnityContainer container = new UnityContainer();container.RegisterSingleton()
            return new UnityIoCAdapter(container);
        }
    }
}
