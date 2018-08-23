using System;
using System.Collections.Generic;
using System.Text;
using Astutia.Common.IoC;
using Astutia.Common.Tests.IoC.Adapter.Specific.TestObjects;
using FluentAssertions;

namespace Astutia.Common.Tests.IoC.Adapter.Specific
{
    public abstract class IoCAdapterTestsBase
    {
        public virtual void WhenRegister_ShouldGetLevel1Properly()
        {
            // Arrange
            IIoCContainer target = this.CreateTarget();
            this.Register(target);

            // Act
            ILevel1 actual = target.Resolve<ILevel1>();

            // Assert
            this.AssertProperObjectTree(actual);
        }

        public virtual void WhenRegister_ShouldGetLevel2Properly()
        {
            // Arrange
            IIoCContainer target = this.CreateTarget();
            this.Register(target);

            // Act
            ILevel2 actual = target.Resolve<ILevel2>();

            // Assert
            this.AssertProperObjectTree(actual);
        }

        public virtual void WhenRegister_ShouldGetLevel3Properly()
        {
            // Arrange
            IIoCContainer target = this.CreateTarget();
            this.Register(target);

            // Act
            ILevel3 actual = target.Resolve<ILevel3>();

            // Assert
            this.AssertProperObjectTree(actual);
        }

        public virtual void WhenRegisterFactory_ShouldGetLevel1Properly()
        {
            // Arrange
            IIoCContainer target = this.CreateTarget();
            this.RegisterFactory(target);

            // Act
            ILevel1 actual = target.Resolve<ILevel1>();

            // Assert
            this.AssertProperObjectTree(actual);
        }

        protected abstract IIoCContainer CreateTarget();

        protected void Register(IIoCContainer container)
        {
            container.Register<ILevel1, Level1Object>(IocRegisterSettings.None);
            container.Register<ILevel2, Level2Object>(IocRegisterSettings.None);
            container.Register<ILevel3, Level3Object>(IocRegisterSettings.None);
        }

        protected void RegisterFactory(IIoCContainer container)
        {
            container.Register<ILevel1>(resolver => {
                return new Level1Object(resolver.Resolve<ILevel2>(), resolver.Resolve<ILevel3>());
            }, IocRegisterSettings.None);
            container.Register<ILevel2, Level2Object>(IocRegisterSettings.None);
            container.Register<ILevel3, Level3Object>(IocRegisterSettings.None);
        }

        protected void AssertProperObjectTree(ILevel root)
        {
            root.IsValid().Should().BeTrue();
        }
    }
}
