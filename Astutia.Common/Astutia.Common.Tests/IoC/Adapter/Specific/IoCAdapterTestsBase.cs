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
        private int factoryLevelInvoked;

        public virtual void TestInitialize()
        {
            this.factoryLevelInvoked = 0;
        }

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
            this.Register(target, 1);

            // Act
            ILevel1 actual = target.Resolve<ILevel1>();

            // Assert
            this.AssertProperObjectTree(actual, 1);
        }

        public virtual void WhenRegisterFactory_ShouldGetLevel2Properly()
        {
            // Arrange
            IIoCContainer target = this.CreateTarget();
            this.Register(target, 2);

            // Act
            ILevel2 actual = target.Resolve<ILevel2>();

            // Assert
            this.AssertProperObjectTree(actual, 2);
        }

        public virtual void WhenRegisterFactory_ShouldGetLevel3Properly()
        {
            // Arrange
            IIoCContainer target = this.CreateTarget();
            this.Register(target, 3);

            // Act
            ILevel3 actual = target.Resolve<ILevel3>();

            // Assert
            this.AssertProperObjectTree(actual, 3);
        }

        protected abstract IIoCContainer CreateTarget();

        protected void Register(IIoCContainer container, int factoryForLevel = 0)
        {
            if (factoryForLevel == 1)
            {
                container.Register<ILevel1>(resolver =>
                {
                    this.factoryLevelInvoked = 1;
                    return new Level1Object(resolver.Resolve<ILevel2>(), resolver.Resolve<ILevel3>());
                }
                , IocRegisterSettings.None);
            }
            else
            {
                container.Register<ILevel1, Level1Object>(IocRegisterSettings.None);
            }

            if (factoryForLevel == 2)
            {
                container.Register<ILevel2>(resolver =>
                {
                    this.factoryLevelInvoked = 2;
                    return new Level2Object(resolver.Resolve<ILevel3>());
                }
               , IocRegisterSettings.None);
            }
            else
            {
                container.Register<ILevel2, Level2Object>(IocRegisterSettings.None);
            }

            if (factoryForLevel == 3)
            {
                container.Register<ILevel3>(resolver =>
                {
                    this.factoryLevelInvoked = 3;
                    return new Level3Object();
                }
               , IocRegisterSettings.None);
            }
            else
            {
                container.Register<ILevel3, Level3Object>(IocRegisterSettings.None);
            }
        }

        protected void AssertProperObjectTree(ILevel root, int factoryLevel = 0)
        {
            root.IsValid().Should().BeTrue();

            if (factoryLevel != 0)
            {
                this.factoryLevelInvoked.Should().Be(factoryLevel);
            }
        }
    }
}
