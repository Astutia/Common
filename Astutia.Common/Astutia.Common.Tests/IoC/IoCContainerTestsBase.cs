using System;
using System.Collections.Generic;
using System.Text;
using Astutia.Common.IoC;
using Astutia.Common.Tests.IoC.TestObjects;
using FluentAssertions;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Astutia.Common.Tests.IoC
{
    public abstract class IoCContainerTestsBase
    {
        private readonly List<int> factoryLevelInvoked = new List<int>();

        public virtual void TestInitialize()
        {
            this.factoryLevelInvoked.Clear();
        }

        [ExpectedException(typeof(InvalidOperationException))]
        public virtual void WhenNoRegistrationAndResolve_ShouldThrowException()
        {
            // Arrange
            IIoCContainer target = this.CreateTarget();
            this.NotifyRegistrationFinished(target);

            // Act
            target.Resolve<ILevel1>();

            // Assert
            Assert.Fail("An exception expected.");
        }

        public virtual void WhenRegister_ShouldGetLevel1Properly()
        {
            // Arrange
            IIoCContainer target = this.CreateTarget();
            RegisterTestObjectContext registration = new RegisterTestObjectContext()
            {
                Container = target,
                Settings = new RegisterLevelContext[]
                {
                    new RegisterLevelContext() { UseFactory = false, Settings = IocRegisterSettings.None },
                    new RegisterLevelContext() { UseFactory = false, Settings = IocRegisterSettings.None },
                    new RegisterLevelContext() { UseFactory = false, Settings = IocRegisterSettings.None }
                }
            };
            this.Register(registration);

            // Act
            ILevel1 actual = target.Resolve<ILevel1>();

            // Assert
            this.AssertProperObjectTree(actual, registration);
        }

        public virtual void WhenRegister_ShouldGetLevel2Properly()
        {
            // Arrange
            IIoCContainer target = this.CreateTarget();
            RegisterTestObjectContext registration = new RegisterTestObjectContext()
            {
                Container = target,
                Settings = new RegisterLevelContext[]
                {
                    new RegisterLevelContext() { UseFactory = false, Settings = IocRegisterSettings.None },
                    new RegisterLevelContext() { UseFactory = false, Settings = IocRegisterSettings.None },
                    new RegisterLevelContext() { UseFactory = false, Settings = IocRegisterSettings.None }
                }
            };
            this.Register(registration);

            // Act
            ILevel2 actual = target.Resolve<ILevel2>();

            // Assert
            this.AssertProperObjectTree(actual, registration);
        }

        public virtual void WhenRegister_ShouldGetLevel3Properly()
        {
            // Arrange
            IIoCContainer target = this.CreateTarget();
            RegisterTestObjectContext registration = new RegisterTestObjectContext()
            {
                Container = target,
                Settings = new RegisterLevelContext[]
                {
                    new RegisterLevelContext() { UseFactory = false, Settings = IocRegisterSettings.None },
                    new RegisterLevelContext() { UseFactory = false, Settings = IocRegisterSettings.None },
                    new RegisterLevelContext() { UseFactory = false, Settings = IocRegisterSettings.None }
                }
            };
            this.Register(registration);

            // Act
            ILevel3 actual = target.Resolve<ILevel3>();

            // Assert
            this.AssertProperObjectTree(actual, registration);
        }

        public virtual void WhenRegisterFactory_ShouldGetLevel1Properly()
        {
            // Arrange
            IIoCContainer target = this.CreateTarget();
            RegisterTestObjectContext registration = new RegisterTestObjectContext()
            {
                Container = target,
                Settings = new RegisterLevelContext[]
                {
                    new RegisterLevelContext() { UseFactory = true, Settings = IocRegisterSettings.None },
                    new RegisterLevelContext() { UseFactory = false, Settings = IocRegisterSettings.None },
                    new RegisterLevelContext() { UseFactory = false, Settings = IocRegisterSettings.None }
                }
            };
            this.Register(registration);

            // Act
            ILevel1 actual = target.Resolve<ILevel1>();

            // Assert
            this.AssertProperObjectTree(actual, registration);
        }

        public virtual void WhenRegisterFactory_ShouldGetLevel2Properly()
        {
            // Arrange
            IIoCContainer target = this.CreateTarget();
            RegisterTestObjectContext registration = new RegisterTestObjectContext()
            {
                Container = target,
                Settings = new RegisterLevelContext[]
                {
                    new RegisterLevelContext() { UseFactory = false, Settings = IocRegisterSettings.None },
                    new RegisterLevelContext() { UseFactory = true, Settings = IocRegisterSettings.None },
                    new RegisterLevelContext() { UseFactory = false, Settings = IocRegisterSettings.None }
                }
            };
            this.Register(registration);

            // Act
            ILevel2 actual = target.Resolve<ILevel2>();

            // Assert
            this.AssertProperObjectTree(actual, registration);
        }

        public virtual void WhenRegisterFactory_ShouldGetLevel3Properly()
        {
            // Arrange
            IIoCContainer target = this.CreateTarget();
            RegisterTestObjectContext registration = new RegisterTestObjectContext()
            {
                Container = target,
                Settings = new RegisterLevelContext[]
                {
                    new RegisterLevelContext() { UseFactory = false, Settings = IocRegisterSettings.None },
                    new RegisterLevelContext() { UseFactory = false, Settings = IocRegisterSettings.None },
                    new RegisterLevelContext() { UseFactory = true, Settings = IocRegisterSettings.None }
                }
            };
            this.Register(registration);

            // Act
            ILevel3 actual = target.Resolve<ILevel3>();

            // Assert
            this.AssertProperObjectTree(actual, registration);
        }

        public virtual void WhenRegisterAsSingleton_ShouldCreateOnce()
        {
            // Arrange
            IIoCContainer target = this.CreateTarget();
            RegisterTestObjectContext registration = new RegisterTestObjectContext()
            {
                Container = target,
                Settings = new RegisterLevelContext[]
                {
                    new RegisterLevelContext() { UseFactory = false, Settings = IocRegisterSettings.Singleton },
                    new RegisterLevelContext() { UseFactory = false, Settings = IocRegisterSettings.None },
                    new RegisterLevelContext() { UseFactory = false, Settings = IocRegisterSettings.None }
                }
            };
            this.Register(registration);

            // Act
            ILevel1 actual1Level1 = target.Resolve<ILevel1>();
            ILevel1 actual2Level1 = target.Resolve<ILevel1>();
            ILevel2 actual1Level2 = target.Resolve<ILevel2>();
            ILevel2 actual2Level2 = target.Resolve<ILevel2>();
            ILevel3 actual1Level3 = target.Resolve<ILevel3>();
            ILevel3 actual2Level3 = target.Resolve<ILevel3>();


            // Assert
            actual1Level1.Should().BeSameAs(actual2Level1);
            actual1Level2.Should().NotBeSameAs(actual2Level2);
            actual1Level3.Should().NotBeSameAs(actual2Level3);
        }

        public virtual void WhenRegisterAsSingleton_ShouldCreateOnce2()
        {
            IIoCContainer target = this.CreateTarget();
            RegisterTestObjectContext registration = new RegisterTestObjectContext()
            {
                Container = target,
                Settings = new RegisterLevelContext[]
                {
                    new RegisterLevelContext() { UseFactory = false, Settings = IocRegisterSettings.Singleton },
                    new RegisterLevelContext() { UseFactory = false, Settings = IocRegisterSettings.Singleton },
                    new RegisterLevelContext() { UseFactory = false, Settings = IocRegisterSettings.None }
                }
            };
            this.Register(registration);

            // Act
            ILevel1 actual1Level1 = target.Resolve<ILevel1>();
            ILevel1 actual2Level1 = target.Resolve<ILevel1>();
            ILevel2 actual1Level2 = target.Resolve<ILevel2>();
            ILevel2 actual2Level2 = target.Resolve<ILevel2>();
            ILevel3 actual1Level3 = target.Resolve<ILevel3>();
            ILevel3 actual2Level3 = target.Resolve<ILevel3>();


            // Assert
            actual1Level1.Should().BeSameAs(actual2Level1);
            actual1Level2.Should().BeSameAs(actual2Level2);
            actual1Level3.Should().NotBeSameAs(actual2Level3);
        }

        public virtual void WhenRegisterFactorySingleton_ShouldGetLevel1Properly()
        {
            // Arrange
            IIoCContainer target = this.CreateTarget();
            RegisterTestObjectContext registration = new RegisterTestObjectContext()
            {
                Container = target,
                Settings = new RegisterLevelContext[]
                {
                    new RegisterLevelContext() { UseFactory = true, Settings = IocRegisterSettings.Singleton },
                    new RegisterLevelContext() { UseFactory = false, Settings = IocRegisterSettings.None },
                    new RegisterLevelContext() { UseFactory = false, Settings = IocRegisterSettings.None }
                }
            };
            this.Register(registration);

            // Act
            ILevel1 actual1 = target.Resolve<ILevel1>();
            ILevel1 actual2 = target.Resolve<ILevel1>();

            // Assert
            actual1.Should().BeSameAs(actual2);
            this.AssertProperObjectTree(actual1, registration);
        }

        public virtual void WhenRegisterFactorySingleton_ShouldGetLevel2Properly()
        {
            // Arrange
            IIoCContainer target = this.CreateTarget();
            RegisterTestObjectContext registration = new RegisterTestObjectContext()
            {
                Container = target,
                Settings = new RegisterLevelContext[]
                {
                    new RegisterLevelContext() { UseFactory = false, Settings = IocRegisterSettings.None },
                    new RegisterLevelContext() { UseFactory = true, Settings = IocRegisterSettings.Singleton },
                    new RegisterLevelContext() { UseFactory = false, Settings = IocRegisterSettings.None }
                }
            };
            this.Register(registration);

            // Act
            ILevel2 actual1 = target.Resolve<ILevel2>();
            ILevel2 actual2 = target.Resolve<ILevel2>();

            // Assert
            actual1.Should().BeSameAs(actual2);
            this.AssertProperObjectTree(actual1, registration);
        }

        public virtual void WhenRegisterFactorySingleton_ShouldGetLevel3Properly()
        {
            // Arrange
            IIoCContainer target = this.CreateTarget();
            RegisterTestObjectContext registration = new RegisterTestObjectContext()
            {
                Container = target,
                Settings = new RegisterLevelContext[]
                {
                    new RegisterLevelContext() { UseFactory = false, Settings = IocRegisterSettings.None },
                    new RegisterLevelContext() { UseFactory = false, Settings = IocRegisterSettings.None },
                    new RegisterLevelContext() { UseFactory = true, Settings = IocRegisterSettings.Singleton }
                }
            };
            this.Register(registration);

            // Act
            ILevel3 actual1 = target.Resolve<ILevel3>();
            ILevel3 actual2 = target.Resolve<ILevel3>();

            // Assert
            actual1.Should().BeSameAs(actual2);
            this.AssertProperObjectTree(actual1, registration);
        }

        protected abstract IIoCContainer CreateTarget();

        protected virtual void NotifyRegistrationFinished(IIoCContainer target)
        {
        }

        private void Register(RegisterTestObjectContext context)
        {
            if (context.Settings[0].UseFactory)
            {
                context.Container.Register<ILevel1>(resolver =>
                {
                    this.factoryLevelInvoked.Add(0);
                    return new Level1Object(resolver.Resolve<ILevel2>(), resolver.Resolve<ILevel3>());
                },
                context.Settings[0].Settings);
            }
            else
            {
                context.Container.Register<ILevel1, Level1Object>(context.Settings[0].Settings);
            }

            if (context.Settings[1].UseFactory)
            {
                context.Container.Register<ILevel2>(resolver =>
                {
                    this.factoryLevelInvoked.Add(1);
                    return new Level2Object(resolver.Resolve<ILevel3>());
                },
                context.Settings[1].Settings);
            }
            else
            {
                context.Container.Register<ILevel2, Level2Object>(context.Settings[1].Settings);
            }

            if (context.Settings[2].UseFactory)
            {
                context.Container.Register<ILevel3>(resolver =>
                {
                    this.factoryLevelInvoked.Add(2);
                    return new Level3Object();
                },
                context.Settings[2].Settings);
            }
            else
            {
                context.Container.Register<ILevel3, Level3Object>(context.Settings[2].Settings);
            }

            this.NotifyRegistrationFinished(context.Container);
        }

        private void AssertProperObjectTree(ILevel root, RegisterTestObjectContext registration)
        {
            root.IsValid().Should().BeTrue();

            for (int i = 0; i < registration.Settings.Length; ++i)
            {
                if (registration.Settings[i].UseFactory)
                {
                    this.factoryLevelInvoked.Count(x => x == i).Should().Be(1);
                }
            }
        }
    }
}
