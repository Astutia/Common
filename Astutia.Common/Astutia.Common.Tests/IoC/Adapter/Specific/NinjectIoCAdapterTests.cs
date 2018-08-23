using System;
using System.Collections.Generic;
using System.Text;
using Astutia.Common.IoC;
using Astutia.Common.IoC.Adapter.Specific;
using Astutia.Common.Tests.IoC.Adapter.Specific.TestObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace Astutia.Common.Tests.IoC.Adapter.Specific
{
    [TestClass]
    public class NinjectIoCAdapterTests : IoCAdapterTestsBase
    {
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

        protected override IIoCContainer CreateTarget()
        {
            IKernel kernel = new StandardKernel();
            return new NinjectIoCAdapter(kernel);
        }
    }
}
