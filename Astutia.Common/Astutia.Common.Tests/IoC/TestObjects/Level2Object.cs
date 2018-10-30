using System;
using System.Collections.Generic;
using System.Text;

namespace Astutia.Common.Tests.IoC.TestObjects
{
    public class Level2Object : ILevel2
    {
        private readonly ILevel3 level3;

        public Level2Object(ILevel3 level3)
        {
            this.level3 = level3;
        }

        public bool IsValid()
        {
            return this.level3 is Level3Object
                   && this.level3.IsValid();
        }
    }
}
