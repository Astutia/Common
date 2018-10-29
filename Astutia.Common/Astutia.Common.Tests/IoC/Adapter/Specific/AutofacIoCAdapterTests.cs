using Astutia.Common.IoC;
using Astutia.Common.IoC.Adapter.Specific;
using Astutia.Common.Tests.IoC.Adapter.Specific.TestObjects;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Astutia.Common.Tests.IoC.Adapter.Specific
{
    [TestClass]
    public class AutofacIoCAdapterTests : IoCAdapterTestsBase
    {
        private ContainerBuilder container;

        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
            this.container = new ContainerBuilder();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void WhenResolveWithouInvokingSetResolveContainer_ShouldThrowException()
        {
            // Arrange
            IIoCContainer target = this.CreateTarget();

            // Act
            target.Resolve<ILevel1>();

            // Assert
            Assert.Fail("An exception is expected.");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void WhenRegisterAfterInvokingSetResolveContainer_ShouldThrowException()
        {
            // Arrange
            IIoCContainer target = this.CreateTarget();
            target.SetResolveContainer(this.container.Build());

            // Act
            target.Register<ILevel1, Level1Object>(IocRegisterSettings.None);

            // Assert
            Assert.Fail("An exception is expected.");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void WhenRegisterFactoryAfterInvokingSetResolveContainer_ShouldThrowException()
        {
            // Arrange
            IIoCContainer target = this.CreateTarget();
            target.SetResolveContainer(this.container.Build());

            // Act
            target.Register<ILevel1>(container => null, IocRegisterSettings.None);

            // Assert
            Assert.Fail("An exception is expected.");
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
            return new AutofacIoCAdapter(this.container);
        }

        protected override void NotifyRegistrationFinished(IIoCContainer target)
        {
            target.SetResolveContainer(this.container.Build());
        }
    }
}
